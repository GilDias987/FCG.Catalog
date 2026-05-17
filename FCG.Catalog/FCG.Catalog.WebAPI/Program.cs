using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using FCG.Catalog.Application.UseCases.Interceptor;
using FCG.Catalog.Application.UseCases.Registration;
using FCG.Catalog.Application.UseCases.Service;
using FCG.Catalog.Infrastructure.Context;
using FCG.Catalog.Infrastructure.Repository;
using FCG.Catalog.WebAPI.Configurations;
using FCG.Catalog.WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configura o ILogger para ler o appsettings.json
builder.AddLogging();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "Api Catalogs - Fiap Cloud Game";
    options.Version = "1.0";
    options.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
    {
        Description = "Bearer token authorization header",
        Type = NSwag.OpenApiSecuritySchemeType.Http,
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer"
    });

    options.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

var sqlConn = builder.Configuration.GetConnectionString("ConnectionStrings");
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration["Mongodbsql:ConnectionString"];
    return new MongoClient(connectionString);
});
builder.Services.AddSingleton<MongoAuditService>();
builder.Services.AddScoped<AuditInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.UseSqlServer(sqlConn);
    options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
});

#region [JWT]

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

#endregion

#region Exception Global
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
#endregion

#region Repository
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IPlataformRepository, PlataformRepository>();
builder.Services.AddScoped<IGenderRepository, GenderRepository>();
builder.Services.AddScoped<IUserGameRepository, UserGameRepository>();
#endregion

#region Elastic
var elasticSettings =
    new ElasticsearchClientSettings(
        new Uri(builder.Configuration["Elastic:Url"])
    )
    .Authentication(
        new BasicAuthentication(
            builder.Configuration["Elastic:Usuario"],
            builder.Configuration["Elastic:Senha"]
        )
    )
    .DefaultIndex("games");

builder.Services.AddSingleton(
    new ElasticsearchClient(elasticSettings)
);

builder.Services.AddScoped<
    IGameSearchService,
    GameSearchService>();

#endregion

builder.Services.AddProblemDetails();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ADMINISTRADOR", policy => policy.RequireRole("ADMINISTRADOR"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseOpenApi();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    var elastic = scope.ServiceProvider.GetRequiredService<IGameSearchService>();
    await elastic.CreateIndexAsync();
}

app.Run();

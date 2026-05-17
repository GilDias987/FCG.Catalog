using FCG.Catalog.Application.Interface.Service;
using FCG.Catalog.Application.UseCases.Behaviour;
using FCG.Catalog.Application.UseCases.Feature.Game.Consumers;
using FCG.Catalog.Application.UseCases.Handler;
using FCG.Catalog.Application.UseCases.Service;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Net.Http.Headers;
using System.Reflection;

namespace FCG.Catalog.Application.UseCases.Registration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<AuthenticationHandler>();
            services.AddSingleton<ICacheService, CacheService>();

            // HttpClient configurado
            services.AddHttpClient<UserApiService>(client =>
            {
                client.BaseAddress = new Uri(configuration["Api:User"]);
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler<AuthenticationHandler>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<PaymentProcessConsumer>();
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(configuration["ServiceBus:ConnectionString"]);

                    cfg.ReceiveEndpoint("payment-process-catalog-queue", e =>
                    {
                        // não criar topology automática (evita topics)
                        e.ConfigureConsumeTopology = false;

                        // evita propriedades não suportadas
                        e.RemoveSubscriptions = true;

                        e.ConfigureConsumer<PaymentProcessConsumer>(context);
                    });
                });
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configurationRedis = configuration["Redis:ConnectionString"];

                var options = ConfigurationOptions.Parse(configurationRedis);

                options.ConnectTimeout = 30000;
                options.SyncTimeout = 30000;
                options.AbortOnConnectFail = false;

                options.Ssl = true;
                options.ReconnectRetryPolicy = new ExponentialRetry(5000);

                return ConnectionMultiplexer.Connect(options);
            });

            return services;
        }
    }
}

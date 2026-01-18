using FCG.Catalog.Application.UseCases.Behaviour;
using FCG.Catalog.Application.UseCases.Feature.Game.Consumers;
using FCG.Catalog.Application.UseCases.Handler;
using FCG.Catalog.Application.UseCases.Service;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

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
            services.AddTransient<AuthenticationHandler>();

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
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["Rabbitmq:Url"], "/", h =>
                    {
                        h.Username(configuration["Rabbitmq:Username"]);
                        h.Password(configuration["Rabbitmq:Password"]);
                    });

                    cfg.ReceiveEndpoint("payment-process-catalog-queue", e =>
                    {
                        e.ConfigureConsumer<PaymentProcessConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}

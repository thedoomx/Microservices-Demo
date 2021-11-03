﻿namespace Oxygen.Startup.Common
{
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Oxygen.Application.Common;
    using Oxygen.Application.Common.Services.Identity;
    using System.Text;
    using System;
    using System.Reflection;
    using AutoMapper;
    using GreenPipes;
    using Hangfire;
    using Hangfire.SqlServer;
    using MassTransit;
    using Microsoft.Data.SqlClient;
    using Microsoft.IdentityModel.Tokens;
    using Ogyxen.Common.Extensions;
    using Oxygen.Application.Common.Mapping;
    using Oxygen.Infrastructure.Common.Messages;
    using Oxygen.Infrastructure.Common.Services;
    using Oxygen.Startup.Common.Infrastructure;
    using Ogyxen.Application.Common;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(
            this IServiceCollection services, 
            IConfiguration configuration,
            bool databaseHealthChecks = true,
            bool messagingHealthChecks = true)
        {
            services
                .AddApplicationSettings(configuration)
                .AddTokenAuthentication(configuration)
                .AddHealth(configuration, databaseHealthChecks, messagingHealthChecks)
                .AddAutoMapperProfile(Assembly.GetCallingAssembly())
                .AddControllers()
                .AddFluentValidation(validation => validation
                    .RegisterValidatorsFromAssemblyContaining<Result>())
                .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static IServiceCollection AddApplicationSettings(
           this IServiceCollection services,
           IConfiguration configuration)
           => services
               .Configure<ApplicationSettings>(
                   configuration.GetSection(nameof(ApplicationSettings)),
                   config => config.BindNonPublicProperties = true);

        public static IServiceCollection AddTokenAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            JwtBearerEvents events = null)
        {
            services
                .AddHttpContextAccessor()
                .AddScoped<ICurrentUserService, CurrentUserService>();

            var secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    if (events != null)
                    {
                        bearer.Events = events;
                    }
                });

            return services;
        }

        public static IServiceCollection AddAutoMapperProfile(
           this IServiceCollection services,
           Assembly assembly)
           => services
               .AddAutoMapper(
                   (_, config) => config
                       .AddProfile(new MappingProfile(assembly)),
                   Array.Empty<Assembly>());

        public static IServiceCollection AddHealth(
            this IServiceCollection services,
            IConfiguration configuration,
            bool databaseHealthChecks = true,
            bool messagingHealthChecks = true)
        {
            var healthChecks = services.AddHealthChecks();

            if (databaseHealthChecks)
            {
                healthChecks
                    .AddSqlServer(configuration.GetDefaultConnectionString());
            }

            if (messagingHealthChecks)
            {
                var messageQueueSettings = MessageQueueSettingsHelper.GetMessageQueueSettings(configuration);
                messageQueueSettings = new MessageQueueSettings("rabbitmq", "rabbitmq", "rabbitmq");

                var messageQueueConnectionString =
                    $"amqp://{messageQueueSettings.UserName}:{messageQueueSettings.Password}@{messageQueueSettings.Host}/";

                healthChecks
                    .AddRabbitMQ(rabbitConnectionString: messageQueueConnectionString);
            }

            return services;
        }

        
    }
}

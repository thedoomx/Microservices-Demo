namespace Oxygen.Identity.Infrastructure
{
    using System;
    using System.Data.SqlClient;
    using GreenPipes;
    using Hangfire;
    using Hangfire.SqlServer;
    using MassTransit;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Ogyxen.Application.Common;
    using Ogyxen.Common.Extensions;
    using Oxygen.Application.Common;
    using Oxygen.Identity.Application;
    using Oxygen.Identity.Infrastructure.Persistence;
    using Oxygen.Infrastructure.Common.Events;
    using Oxygen.Infrastructure.Common.Messages;
    using Oxygen.Infrastructure.Common.Persistence;
    using Oxygen.Infrastructure.Common.Services;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDatabase(configuration)
                .AddIdentity(configuration)
                .AddTransient<IEventDispatcher, EventDispatcher>()
                .AddMessaging(
                    configuration);

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, MyIdentityDbContext>()
                .AddDbContext<MyIdentityDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(MyIdentityDbContext).Assembly.FullName)
                            .EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null)))
                .AddTransient<IInitializer, DatabaseInitializer>();

        private static IServiceCollection AddIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<MyIdentityDbContext>();

            //var secret = configuration
            //    .GetSection(nameof(ApplicationSettings))
            //    .GetValue<string>(nameof(ApplicationSettings.Secret));

            //var key = Encoding.ASCII.GetBytes(secret);

            //services
            //    .AddAuthentication(authentication =>
            //    {
            //        authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(bearer =>
            //    {
            //        bearer.RequireHttpsMetadata = false;
            //        bearer.SaveToken = true;
            //        bearer.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(key),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //    });

            services.AddTransient<IIdentity, IdentityService>();
            services.AddTransient<IJwtTokenGenerator, JwtTokenGeneratorService>();

            return services;
        }

        public static IServiceCollection AddMessaging(
            this IServiceCollection services,
            IConfiguration configuration,
            bool usePolling = true,
            params Type[] consumers)
        {
            services.AddTransient<IPublisher, Publisher>();
            services.AddTransient<IMessageService, MessageService>();

            var messageQueueSettings = MessageQueueSettingsHelper.GetMessageQueueSettings(configuration);
            messageQueueSettings = new MessageQueueSettings("localhost", "", "");

            services
                .AddMassTransit(mt =>
                {
                    consumers.ForEach(consumer => mt.AddConsumer(consumer));

                    mt.AddBus(context => Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.Host(messageQueueSettings.Host, host =>
                        {
                            host.Username(messageQueueSettings.UserName);
                            host.Password(messageQueueSettings.Password);
                        });

                        rmq.UseHealthCheck(context);

                        consumers.ForEach(consumer => rmq.ReceiveEndpoint(consumer.FullName, endpoint =>
                        {
                            endpoint.PrefetchCount = 6;
                            endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                            endpoint.ConfigureConsumer(context, consumer);
                        }));
                    }));
                })
                .AddMassTransitHostedService();

            if (usePolling)
            {
                CreateHangfireDatabase(configuration);

                services
                    .AddHangfire(config => config
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage(
                            configuration.GetCronJobsConnectionString(),
                            new SqlServerStorageOptions
                            {
                                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                QueuePollInterval = TimeSpan.Zero,
                                UseRecommendedIsolationLevel = true,
                                DisableGlobalLocks = true
                            }));

                services.AddHangfireServer();

                services.AddHostedService<MessagesHostedService>();
            }

            return services;
        }

        private static void CreateHangfireDatabase(IConfiguration configuration)
        {
            var connectionString = configuration.GetCronJobsConnectionString();

            var dbName = connectionString
                .Split(";")[1]
                .Split("=")[1];

            using var connection = new SqlConnection(connectionString.Replace(dbName, "master"));

            connection.Open();

            using var command = new SqlCommand(
                $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}') create database [{dbName}];",
                connection);

            command.ExecuteNonQuery();
        }

    }
}

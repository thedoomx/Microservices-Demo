namespace Oxygen.Company.Infrastructure
{
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Oxygen.Application.Common.Contracts;
    using Oxygen.Domain.Common;
    using Oxygen.Infrastructure.Common.Events;
    using Oxygen.Infrastructure.Common.Persistence;
    using Oxygen.Company.Infrastructure.Persistence;
    using System;
    using Oxygen.Infrastructure.Common.Services;
    using Oxygen.Application.Common;
    using Oxygen.Infrastructure.Common.Messages;
    using Ogyxen.Common.Extensions;
    using Ogyxen.Application.Common;
    using GreenPipes;
    using Hangfire;
    using Hangfire.SqlServer;
    using MassTransit;
    using Microsoft.Data.SqlClient;
    using Oxygen.Company.Infrastructure.Messages;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
           => services
                .AddDatabase(configuration)
                .AddRepositories()
                .AddTransient<IEventDispatcher, EventDispatcher>()
                .AddMessaging(
                    configuration,
                    consumers: typeof(UserCreatedConsumer));

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddScoped<DbContext, CompanyDbContext>()
                .AddDbContext<CompanyDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(CompanyDbContext).Assembly.FullName)
                            .EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null)))
                .AddScoped<ICompanyDbContext>(provider => provider.GetService<CompanyDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer>();

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IDomainRepository<>))
                        .AssignableTo(typeof(IQueryRepository<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

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

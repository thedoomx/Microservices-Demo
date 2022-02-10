namespace Oxygen.Startup.Common.Infrastructure
{
	using GreenPipes;
	using Hangfire;
	using Hangfire.SqlServer;
	using MassTransit;
	using Microsoft.Data.SqlClient;
	using Oxygen.Infrastructure.Common.Services;
	using Oxygen.Common.Extensions;
	using Oxygen.Application.Common;
	using Oxygen.Infrastructure.Common.Messages;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Configuration;
	using System;

	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddMessaging(
		  this IServiceCollection services,
		  IConfiguration configuration,
		  bool usePolling = true,
		  bool addMessageServices = true,
		  params Type[] consumers)
		{
			if (addMessageServices)
			{
				services.AddTransient<IPublisher, Publisher>();
				services.AddTransient<IMessageService, MessageService>();
			}

			var messageQueueSettings = MessageQueueSettingsHelper.GetMessageQueueSettings(configuration);
#if (DEBUG)
			messageQueueSettings = new MessageQueueSettings("localhost", "", "");
#endif

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

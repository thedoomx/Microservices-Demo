namespace Oxygen.Company.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Oxygen.Application.Common.Contracts;
    using Oxygen.Domain.Common;
    using Oxygen.Infrastructure.Common.Events;
    using Oxygen.Infrastructure.Common.Persistence;
    using Oxygen.Company.Infrastructure.Persistence;
    using System;
    using Oxygen.Company.Infrastructure.Messages;
	using Oxygen.Startup.Common.Infrastructure;

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
    }
}

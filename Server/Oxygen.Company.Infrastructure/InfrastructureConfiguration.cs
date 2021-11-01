﻿namespace Oxygen.Company.Infrastructure
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

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
           => services
               .AddDatabase(configuration)
               .AddRepositories()
               .AddTransient<IEventDispatcher, EventDispatcher>();

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<CompanyDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(CompanyDbContext).Assembly.FullName)))
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

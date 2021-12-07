namespace Oxygen.Domain.Common
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, string domainAssembly)
            => services
                .AddFactories(domainAssembly)
                .AddBuilds(domainAssembly)
                .AddInitialData(domainAssembly);
        //.AddTransient<IRentingScheduleService, RentingScheduleService>();

        private static IServiceCollection AddFactories(this IServiceCollection services, string domainAssembly)
            => services
                .Scan(scan => scan
                    .FromAssemblies(AppDomain.CurrentDomain.Load(domainAssembly))
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IFactory<>)))
                    .AsMatchingInterface()
                    .WithTransientLifetime());

        private static IServiceCollection AddBuilds(this IServiceCollection services, string domainAssembly)
            => services
                .Scan(scan => scan
                    .FromAssemblies(AppDomain.CurrentDomain.Load(domainAssembly))
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IBuild<>)))
                    .AsMatchingInterface()
                    .WithTransientLifetime());

        private static IServiceCollection AddInitialData(this IServiceCollection services, string domainAssembly)
            => services
                .Scan(scan => scan
                    .FromAssemblies(AppDomain.CurrentDomain.Load(domainAssembly))
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IInitialData)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
    }
}

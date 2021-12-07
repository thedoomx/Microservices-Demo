namespace Oxygen.Application.Common
{
    using System;
    using System.Reflection;
    using AutoMapper;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Oxygen.Application.Common;
    using Oxygen.Application.Common.Behaviours;
    using Oxygen.Application.Common.Mapping;

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration,
            string applicationAssembly)
            => services
                .Configure<ApplicationSettings>(
                    configuration.GetSection(nameof(ApplicationSettings)),
                    options => options.BindNonPublicProperties = true)
                .AddAutoMapperProfile(AppDomain.CurrentDomain.Load(applicationAssembly))
                .AddMediatR(AppDomain.CurrentDomain.Load(applicationAssembly))
                .AddEventHandlers()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        private static IServiceCollection AddEventHandlers(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                    .AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

        public static IServiceCollection AddAutoMapperProfile(
           this IServiceCollection services,
           Assembly assembly)
           => services
               .AddAutoMapper(
                   (_, config) =>
                    config
                        .AddProfile(new MappingProfile(assembly)),
                        Array.Empty<Assembly>()
                   );
    }
}

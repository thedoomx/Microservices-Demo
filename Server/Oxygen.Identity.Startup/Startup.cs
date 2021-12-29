namespace Oxygen.Identity.Startup
{
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Oxygen.Common.Constants;
    using Oxygen.Application.Common;
    using Oxygen.Startup.Common;
    using Oxygen.Startup.Common.Infrastructure;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddApplication(this.Configuration, GlobalConstants.Assembly.Identity_Application)
                .AddInfrastructure(this.Configuration)
                .AddWebComponents(this.Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
           => app
               .UseWebService(env)
               .Initialize();
    }
}

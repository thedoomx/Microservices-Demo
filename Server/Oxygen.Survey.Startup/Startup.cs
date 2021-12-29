namespace Oxygen.Survey.Startup
{
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Oxygen.Common.Constants;
    using Oxygen.Application.Common;
    using Oxygen.Domain.Common;
    using Oxygen.Startup.Common;
    using Oxygen.Startup.Common.Infrastructure;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
              => services
                  .AddDomain(GlobalConstants.Assembly.Survey_Domain)
                  .AddApplication(this.Configuration, GlobalConstants.Assembly.Survey_Application)
                  .AddInfrastructure(this.Configuration)
                  .AddWebComponents(this.Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
           => app
               .UseWebService(env)
               .Initialize();
    }
}

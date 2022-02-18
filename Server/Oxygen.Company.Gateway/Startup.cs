namespace Oxygen.Company.Gateway
{
	using System.Reflection;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Oxygen.Application.Common;
	using Oxygen.Application.Common.Services.Identity;
	using Oxygen.Company.Gateway.Services;
	using Oxygen.Company.Gateway.Services.Company;
	using Oxygen.Company.Gateway.Services.Identity;
	using Oxygen.Company.Gateway.Services.Survey;
	using Oxygen.Startup.Common;
	using Oxygen.Startup.Common.Infrastructure;
	using Refit;

	public class Startup
	{
		public Startup(IConfiguration configuration) => this.Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var serviceEndpoints = this.Configuration
				.GetSection(nameof(ServiceEndpoints))
				.Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

			services
				.AddHealth(this.Configuration, false, false)
				.AddSwagger(true)
				.AddAutoMapperProfile(Assembly.GetExecutingAssembly())
				.AddTokenAuthentication(this.Configuration)
				.AddScoped<ICurrentTokenService, CurrentTokenService>()
				.AddTransient<JwtHeaderAuthenticationMiddleware>()
				.AddControllers();

			services
				.AddRefitClient<IIdentityService>()
				.WithConfiguration(serviceEndpoints.Identity);

			services
				.AddRefitClient<ICompanyService>()
				.WithConfiguration(serviceEndpoints.Company);


			services
				.AddRefitClient<ISurveyService>()
				.WithConfiguration(serviceEndpoints.Survey);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
			=> app.UseWebService(env);
		//{
		//	if (env.IsDevelopment())
		//	{
		//		app.UseDeveloperExceptionPage();
		//	}

		//	app.UseHttpsRedirection();

		//	app.UseRouting();

		//	app.UseJwtHeaderAuthentication();

		//	app.UseAuthorization();

		//	app.UseEndpoints(endpoints =>
		//	{
		//		endpoints.MapControllers();
		//	});
		//}
	}
}

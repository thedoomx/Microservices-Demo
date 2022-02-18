namespace Oxygen.Startup.Common
{
	using FluentValidation.AspNetCore;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Oxygen.Application.Common;
	using Oxygen.Application.Common.Services.Identity;
	using System.Text;
	using Oxygen.Common.Extensions;
	using Microsoft.IdentityModel.Tokens;
	using Microsoft.OpenApi.Models;
	using System;
	using Microsoft.Net.Http.Headers;

	public static class WebConfiguration
	{
		public static IServiceCollection AddWebComponents(
			this IServiceCollection services,
			IConfiguration configuration,
			bool databaseHealthChecks = true,
			bool messagingHealthChecks = true,
			bool swagger = true)
		{
			services
				.AddSwagger(swagger)
				.AddApplicationSettings(configuration)
				.AddTokenAuthentication(configuration)
				.AddHealth(configuration, databaseHealthChecks, messagingHealthChecks)
				.AddControllers()
				.AddFluentValidation(validation => validation
				.RegisterValidatorsFromAssemblyContaining<Result>())
				.AddNewtonsoftJson();

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			return services;
		}

		public static IServiceCollection AddApplicationSettings(
		   this IServiceCollection services,
		   IConfiguration configuration)
		   => services
			   .Configure<ApplicationSettings>(
				   configuration.GetSection(nameof(ApplicationSettings)),
				   config => config.BindNonPublicProperties = true);

		public static IServiceCollection AddTokenAuthentication(
			this IServiceCollection services,
			IConfiguration configuration,
			JwtBearerEvents events = null)
		{
			services
				.AddHttpContextAccessor()
				.AddScoped<ICurrentUserService, CurrentUserService>();

			var secret = configuration
				.GetSection(nameof(ApplicationSettings))
				.GetValue<string>(nameof(ApplicationSettings.Secret));

			var key = Encoding.ASCII.GetBytes(secret);

			services
				.AddAuthentication(authentication =>
				{
					authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(bearer =>
				{
					bearer.RequireHttpsMetadata = false;
					bearer.SaveToken = true;
					bearer.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false
					};

					if (events != null)
					{
						bearer.Events = events;
					}
				});

			return services;
		}

		public static IServiceCollection AddHealth(
			this IServiceCollection services,
			IConfiguration configuration,
			bool databaseHealthChecks = true,
			bool messagingHealthChecks = true)
		{
			var healthChecks = services.AddHealthChecks();

			if (databaseHealthChecks)
			{
				healthChecks
					.AddSqlServer(configuration.GetDefaultConnectionString());
			}

			if (messagingHealthChecks)
			{
				var messageQueueSettings = MessageQueueSettingsHelper.GetMessageQueueSettings(configuration);
#if (DEBUG)
				messageQueueSettings = new MessageQueueSettings("localhost", "", "");
#endif

				var messageQueueConnectionString =
					$"amqp://{messageQueueSettings.UserName}:{messageQueueSettings.Password}@{messageQueueSettings.Host}/";

				healthChecks
					.AddRabbitMQ(rabbitConnectionString: messageQueueConnectionString);
			}

			return services;
		}

		public static IServiceCollection AddSwagger(
			this IServiceCollection services, bool swagger)
		{
			if (swagger == true)
			{
				services.AddSwaggerGen(
					c =>
					{
						c.SwaggerDoc("v1", new OpenApiInfo { Title = "OxygenApiPlayground", Version = "v1" });
						c.AddSecurityDefinition(
							"token",
							new OpenApiSecurityScheme
							{
								Type = SecuritySchemeType.Http,
								BearerFormat = "JWT",
								Scheme = "Bearer",
								In = ParameterLocation.Header,
								Name = HeaderNames.Authorization
							}
						);
						c.AddSecurityRequirement(
							new OpenApiSecurityRequirement
							{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "token"
						},
					},
					Array.Empty<string>()
				}
							}
						);
					}
				);
			}

			return services;
		}

	}
}

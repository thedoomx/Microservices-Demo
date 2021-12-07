namespace Oxygen.Startup.Common.Infrastructure
{
    using System;
    using System.Net;
    using System.Net.Http.Headers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Oxygen.Application.Common.Services.Identity;
    using Polly;

    using static Oxygen.Common.Constants.GlobalConstants;

    public static class HttpClientBuilderExtensions
    {
        public static void WithConfiguration(
            this IHttpClientBuilder httpClientBuilder,
            string baseAddress)
            => httpClientBuilder
                .ConfigureHttpClient((serviceProvider, client) =>
                {
                    client.BaseAddress = new Uri(baseAddress);

                    var requestServices = serviceProvider
                        .GetService<IHttpContextAccessor>()
                        ?.HttpContext
                        .RequestServices;

                    var currentToken = requestServices
                        ?.GetService<ICurrentTokenService>()
                        ?.Get();

                    if (currentToken == null)
                    {
                        return;
                    }

                    var authorizationHeader = new AuthenticationHeaderValue(AuthorizationHeaderValuePrefix, currentToken);
                    client.DefaultRequestHeaders.Authorization = authorizationHeader;
                })
                .AddTransientHttpErrorPolicy(policy => policy
                    .OrResult(result => result.StatusCode == HttpStatusCode.NotFound)
                    .WaitAndRetryAsync(5, retry => TimeSpan.FromSeconds(Math.Pow(2, retry))))
                .AddTransientHttpErrorPolicy(policy => policy
                    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
    }
}

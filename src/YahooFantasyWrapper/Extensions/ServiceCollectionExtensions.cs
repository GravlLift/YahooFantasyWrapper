using System;
using Microsoft.Extensions.Configuration;
using Polly;
using YahooFantasyWrapper;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Configuration;
using YahooFantasyWrapper.Query.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddYahooFantasyWrapper(this IServiceCollection services, Action<YahooConfiguration> configuration)
        {
            services.AddHttpClient<YahooQueryProvider>(client =>
            {
                client.BaseAddress = new Uri("https://fantasysports.yahooapis.com");
            });
            services.AddHttpClient<IYahooAuthClient, YahooAuthClient>()
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(2, (_) => TimeSpan.FromSeconds(2)));

            return services.AddScoped<YahooFantasyContext>()
                .AddSingleton(_ =>
                {
                    var config = new YahooConfiguration();
                    configuration(config);
                    return config;
                });
        }
    }
}

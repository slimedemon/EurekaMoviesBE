using Polly;

namespace EurekaMovieBE.HttpClientCustom;

public static class HttpClientCustomExtensions
{

    #region Public Methods

    public static IServiceCollection AddHttpClientCustom(this IServiceCollection services, HttpClientOption clientConfig)
    {
        const string clientName = "EurekaMovieBE" ;

        var throttlerPolicy = Policy.BulkheadAsync<HttpResponseMessage>(10000, Int32.MaxValue);
        services.AddHttpClient<IHttpClientCustom, HttpClientCustom>(clientName, c =>
            {
                c.BaseAddress = new Uri(clientConfig.Url);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.Timeout = TimeSpan.FromMinutes(clientConfig.HttpClientTimeout);
            })
            .AddPolicyHandler(throttlerPolicy)
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10000,
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(clientConfig.HttpClientTimeout * 2));

        return services;
    }
    
    #endregion
}
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Copper.HttpClients.Abstract;
using Soenneker.Copper.OpenApiClientUtil.Abstract;
using Soenneker.Copper.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Copper.OpenApiClientUtil;

///<inheritdoc cref="ICopperOpenApiClientUtil"/>
public sealed class CopperOpenApiClientUtil : ICopperOpenApiClientUtil
{
    private readonly AsyncSingleton<CopperOpenApiClient> _client;

    public CopperOpenApiClientUtil(ICopperOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<CopperOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Copper:ApiKey");
            string authHeaderName = configuration["Copper:AuthHeaderName"] ?? "X-PW-AccessToken";
            string authHeaderValueTemplate = configuration["Copper:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

            return new CopperOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<CopperOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Copper.HttpClients.Abstract;
using Soenneker.Copper.OpenApiClientUtil.Abstract;
using Soenneker.Copper.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Copper.OpenApiClientUtil;

/// <inheritdoc cref="ICopperOpenApiClientUtil"/>
public sealed class CopperOpenApiClientUtil : ICopperOpenApiClientUtil
{
    private readonly AsyncSingleton<ClientState> _client;

    public CopperOpenApiClientUtil(ICopperOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<ClientState>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new ClientState(new CopperOpenApiClient(requestAdapter), requestAdapter);
        });
    }

    public async ValueTask<CopperOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        ClientState state = await _client.Get(cancellationToken).NoSync();
        return state.Client;
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }

    private sealed class ClientState : IDisposable
    {
        private readonly HttpClientRequestAdapter _requestAdapter;

        public CopperOpenApiClient Client { get; }

        public ClientState(CopperOpenApiClient client, HttpClientRequestAdapter requestAdapter)
        {
            Client = client;
            _requestAdapter = requestAdapter;
        }

        public void Dispose()
        {
            _requestAdapter.Dispose();
        }
    }
}

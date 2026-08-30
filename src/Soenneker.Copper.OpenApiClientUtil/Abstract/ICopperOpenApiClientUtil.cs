using Soenneker.Copper.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Copper.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a lazily created Copper OpenAPI client for the service lifetime.
/// </summary>
public interface ICopperOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the cached, configured Copper OpenAPI client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the generated client.</returns>
    ValueTask<CopperOpenApiClient> Get(CancellationToken cancellationToken = default);
}

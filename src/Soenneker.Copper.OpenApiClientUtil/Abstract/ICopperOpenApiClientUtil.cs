using Soenneker.Copper.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Copper.OpenApiClientUtil.Abstract;
/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ICopperOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured copper OpenAPI Client used by the Copper OpenAPI Client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested copper OpenAPI Client.</returns>
    ValueTask<CopperOpenApiClient> Get(CancellationToken cancellationToken = default);
}

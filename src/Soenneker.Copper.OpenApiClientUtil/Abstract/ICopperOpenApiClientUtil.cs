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
    ValueTask<CopperOpenApiClient> Get(CancellationToken cancellationToken = default);
}

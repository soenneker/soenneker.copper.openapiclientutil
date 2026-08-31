[![](https://img.shields.io/nuget/v/soenneker.copper.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.copper.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.copper.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.copper.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.copper.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.copper.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.copper.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.copper.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Copper.OpenApiClientUtil

Provides a lazily created, reusable Copper Kiota client backed by the configured Copper HTTP provider.

## Install

```bash
dotnet add package Soenneker.Copper.OpenApiClientUtil
```

## Configuration

```json
{
  "Copper": {
    "ApiKey": "your-api-key",
    "Email": "token-owner@example.com"
  }
}
```

The underlying HTTP package sends Copper's required `X-PW-AccessToken`, `X-PW-Application`, and `X-PW-UserEmail` headers. It also supports `Copper:ClientBaseUrl`, `Copper:Application`, `Copper:AuthHeaderName`, and `Copper:AuthHeaderValueTemplate`.

## Registration

```csharp
using Soenneker.Copper.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddCopperOpenApiClientUtilAsSingleton();
```

Use `AddCopperOpenApiClientUtilAsScoped()` when each application scope should cache its own generated client while sharing the singleton HTTP provider.

## Usage

```csharp
using Soenneker.Copper.OpenApiClientUtil.Abstract;

public sealed class CopperAccountReader(ICopperOpenApiClientUtil clientUtil)
{
    public async ValueTask<string?> Get(CancellationToken cancellationToken)
    {
        var client = await clientUtil.Get(cancellationToken);
        return await client.Account.GetAsync(cancellationToken: cancellationToken);
    }
}
```

`Get` initializes the client once for the utility's lifetime. Concurrent callers share that initialization and receive the same client instance.

## Practical notes

- Configuration is captured when the underlying HTTP client is first created. Recreate the service lifetime to apply changed credentials or a changed base URL.
- Both registrations use a singleton HTTP provider. Disposing a scoped utility releases its generated-client reference without removing the shared `HttpClient`; the HTTP provider disposes that client when the application container shuts down.
- Some generated endpoints return JSON as `string?` because the source Postman collection lacks a strong response schema.
- Redact the API key, token-owner email, and Copper authentication headers from logs and traces.

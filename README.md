[![](https://img.shields.io/nuget/v/soenneker.copper.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.copper.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.copper.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.copper.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.copper.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.copper.openapiclientutil/)

# Soenneker.Copper.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Copper.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Copper.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddCopperOpenApiClientUtilAsSingleton();
```

Adds `CopperOpenApiClientUtil` as a singleton service.

## What you get

- `ICopperOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `CopperOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `CopperOpenApiClientUtilRegistrar.AddCopperOpenApiClientUtilAsSingleton(services)` | Adds `CopperOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `CopperOpenApiClientUtilRegistrar.AddCopperOpenApiClientUtilAsScoped(services)` | Adds `CopperOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.

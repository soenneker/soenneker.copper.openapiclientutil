using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Copper.HttpClients.Registrars;
using Soenneker.Copper.OpenApiClientUtil.Abstract;

namespace Soenneker.Copper.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class CopperOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="CopperOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddCopperOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddCopperOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ICopperOpenApiClientUtil, CopperOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="CopperOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddCopperOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddCopperOpenApiHttpClientAsSingleton()
                .TryAddScoped<ICopperOpenApiClientUtil, CopperOpenApiClientUtil>();

        return services;
    }
}

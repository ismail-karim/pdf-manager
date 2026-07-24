using Microsoft.Extensions.DependencyInjection;
using PDF.Manager.Domain.Services;
using PDF.Manager.Infrastructure.Pdf;

namespace PDF.Manager.Infrastructure.DependencyInjection;

/// <summary>
/// Registers the infrastructure layer services into the dependency injection container.
/// </summary>
public static class InfrastructureServiceCollectionExtensions
{
    /// <summary>
    /// Adds the PDF.Manager infrastructure implementations to the service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The same service collection, to allow fluent chaining.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // The merge service is stateless and thread-safe, so a single shared instance is enough.
        services.AddSingleton<IPdfMergeService, PdfSharpMergeService>();

        return services;
    }
}

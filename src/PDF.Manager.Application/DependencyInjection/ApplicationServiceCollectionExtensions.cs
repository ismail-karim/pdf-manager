using Microsoft.Extensions.DependencyInjection;
using PDF.Manager.Application.Abstractions;
using PDF.Manager.Application.UseCases;

namespace PDF.Manager.Application.DependencyInjection;

/// <summary>
/// Registers the application layer services into the dependency injection container.
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    /// <summary>
    /// Adds the PDF.Manager application use cases to the service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The same service collection, to allow fluent chaining.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IMergePdfDocumentsUseCase, MergePdfDocumentsUseCase>();

        return services;
    }
}

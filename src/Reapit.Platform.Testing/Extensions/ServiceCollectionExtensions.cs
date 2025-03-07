using Microsoft.Extensions.DependencyInjection;

namespace Reapit.Platform.Testing.Extensions;

/// <summary>Extension methods for <see cref="IServiceCollection"/> objects.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Remove services of type <typeparamref name="TService"/> from the services collection. Useful when replacing
    /// services in the WebApplicationFactory for an API.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <typeparam name="TService">The type of service to remove.</typeparam>
    public static void RemoveServiceForType<TService>(this IServiceCollection services)
    {
        var service = services.SingleOrDefault(s => s.ServiceType == typeof(TService));
        if (service is not null)
            services.Remove(service);
    }
}
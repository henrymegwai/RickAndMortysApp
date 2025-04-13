using Microsoft.Extensions.DependencyInjection;

namespace RickAndMorty.Application;

public static class ApplicationServiceRegistration
{
    public static void AddApplicationServices(
        this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServiceRegistration).Assembly;
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
    }
}
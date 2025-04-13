using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Application.Abstractions;
using RickAndMorty.Infrastructure.Services;
using RickAndMorty.Persistence;
using RickAndMorty.Persistence.Repositories;

namespace RickAndMorty.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static void AddInfrastructureServices(
        this IServiceCollection services,
        string connectionString)
    {
        AddDatabaseSetUp(services, connectionString);
        
        services.AddHttpClient();
        
        services.AddScoped<IRickAndMortyService, RickAndMortyService>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();
    }
    
    private static void AddDatabaseSetUp(this IServiceCollection services, string connectionString)
    {
        
        services.AddDbContextPool<ApplicationDbContext>((optionsBuilder) =>
        {
            optionsBuilder.UseSqlServer(connectionString);

        });
        services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>().Database.EnsureCreated();
    }
}
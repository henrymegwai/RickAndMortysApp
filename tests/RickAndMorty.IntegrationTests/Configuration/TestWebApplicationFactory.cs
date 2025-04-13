using DotNet.Testcontainers.Builders;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using RickAndMorty.Data.Entities;
using RickAndMorty.Persistence;
using System.Data.Common;
using Testcontainers.MsSql;

namespace RickAndMorty.IntegrationTests.Configuration;

public class TestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
         .WithImage("mcr.microsoft.com/mssql/server:latest")
        .WithPassword("YourStrong@Passw0rd")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
        .Build();

    private readonly DbConnection _dbConnection = null!;
    private readonly Respawner _respawner = null!;

    public HttpClient HttpClient { get; private set; } = null!;

    public IMemoryCache MemoryCache { get; private set; } = null!;

   
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            builder.UseSetting("--parentprocessid", Environment.ProcessId.ToString());

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_msSqlContainer.GetConnectionString()));
        });

        builder.ConfigureTestServices(services =>
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());
            services.AddMemoryCache();
            services.AddScoped<IMemoryCache, MemoryCache>();
            services.AddScoped(provider =>
            {
                var options = provider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
                return new ApplicationDbContext(options);
            });
        });

        base.ConfigureWebHost(builder);
    }

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();

        HttpClient = CreateClient();

        await using var scope = Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();



        scope.ServiceProvider.GetRequiredService<IMediator>();
        MemoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

        // Seed the database with test data
        await SeedDatabaseAsync(dbContext);

    }

    public async Task DisposeAsync()
    {
        // Stop and clean up the MsSql container
        await _msSqlContainer.DisposeAsync();
        await base.DisposeAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }


    private async Task SeedDatabaseAsync(ApplicationDbContext dbContext)
    {
        // Add test data to the Characters table
        var characters = new List<Character>
        {
            new Character
            {
                Name = "Rick Sanchez",
                Status = "Alive",
                Species = "Human",
                Gender = "Male",
                OriginName = "Earth",
                OriginUrl = "https://example.com/origin",
                LocationName = "Earth",
                LocationUrl = "https://example.com/location",
                Url = "https://example.com/character/1",
                Image = "https://example.com/image",
                Created = DateTime.UtcNow
            },
            new Character
            {
                Name = "Morty Smith",
                Status = "Alive",
                Species = "Human",
                Gender = "Male",
                OriginName = "Earth",
                OriginUrl = "https://example.com/origin",
                LocationName = "Earth",
                LocationUrl = "https://example.com/location",
                Url = "https://example.com/character/2",
                Image = "https://example.com/image",
                Created = DateTime.UtcNow
            }
        };

        dbContext.Characters.AddRange(characters);
        await dbContext.SaveChangesAsync();
    }

}
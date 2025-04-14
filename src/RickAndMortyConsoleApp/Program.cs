using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RickAndMorty.Application;
using RickAndMorty.Application.Common.Enums;
using RickAndMorty.Application.Features.Character.Queries;
using RickAndMorty.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
       var assembly = typeof(Program).Assembly;
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        var connectionString = "Server=localhost\\SQLEXPRESS;Database=RickAndMortyDB;Trusted_Connection=True;TrustServerCertificate=True;";
        services.AddApplicationServices();
        services.AddInfrastructureServices(connectionString);
    })
    .Build();

try
{
   
    var mediator = host.Services.GetRequiredService<IMediator>();
    
    var cts = new CancellationTokenSource();

    await mediator.Send(new GetCharacterByStatusFromServiceQuery(CharacterStatus.Alive), cts.Token);

    Console.WriteLine("Operation completed successfully!");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
finally
{
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
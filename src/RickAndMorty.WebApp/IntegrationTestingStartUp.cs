using Microsoft.Extensions.Caching.Memory;


namespace RickAndMorty.WebApp
{
    public class IntegrationTestingStartUp: Program
    {
        public IntegrationTestingStartUp() { }


        public void Configure()
        {
            

            var builder = WebApplication.CreateBuilder();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            builder.Services.AddMvcCore();
            builder.Services.AddMvc();

            var assembly = typeof(Program).Assembly;
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<IMemoryCache, MemoryCache>();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
        }
    }
}

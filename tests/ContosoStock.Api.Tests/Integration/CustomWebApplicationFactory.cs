using ContosoStock.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoStock.Api.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ContosoStockDbContext>));
            
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<ContosoStockDbContext>(options =>
            {
                options.UseNpgsql("Host=db;Database=ContosoStockDb_Tests;Username=admin;Password=admin");
            });
            
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ContosoStockDbContext>();
            dbContext.Database.Migrate();
        });
    }
}
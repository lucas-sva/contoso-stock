using ContosoStock.Domain.Fulfillment.Ports.Contracts;
using ContosoStock.Infrastructure.Persistence.Contexts;
using ContosoStock.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoStock.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ContosoStockDbContext>(options => options.UseSqlServer(connectionString,
            b => b.MigrationsAssembly(typeof(ContosoStockDbContext).Assembly)));

        services.AddScoped<IStockLotRepository, StockLotRepository>();
        
        return services;
    }
}
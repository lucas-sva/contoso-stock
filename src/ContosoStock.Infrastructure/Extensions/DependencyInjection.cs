using ContosoStock.Domain.Fulfillment.Ports.ACL;
using ContosoStock.Domain.Fulfillment.Ports.Contracts;
using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;
using ContosoStock.Infrastructure.Adapters.Sales;
using ContosoStock.Infrastructure.Persistence.Contexts;
using ContosoStock.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoStock.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Database
        services.AddDbContext<ContosoStockDbContext>(options => options.UseInMemoryDatabase("ContosoStock"));
        
        
        services.AddScoped<IStockLotRepository, StockLotRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // ACL
        services.AddScoped<ISalesIntegration, SalesIntegrationStub>();
        
        return services;
    }
}
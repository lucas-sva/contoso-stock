using System.Data;
using ContosoStock.Application.Fulfillment.Queries.Contracts;
using ContosoStock.Domain.Fulfillment.Ports.ACL;
using ContosoStock.Domain.Fulfillment.Ports.Contracts;
using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;
using ContosoStock.Infrastructure.Adapters.Sales;
using ContosoStock.Infrastructure.Persistence.Contexts;
using ContosoStock.Infrastructure.Persistence.Queries;
using ContosoStock.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace ContosoStock.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Commands -> EF + Postgres
        services.AddDbContext<ContosoStockDbContext>(options => options.UseNpgsql(connectionString));
        
        // Queries -> Dapper + Dto
        services.AddScoped<IDbConnection>( _ => new NpgsqlConnection(connectionString));
        
        // Query CQRS
        services.AddScoped<IGetStockBySkuQuery, GetStockBySkuQuery>();
        
        // Domain services
        services.AddScoped<IStockLotRepository, StockLotRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISalesIntegration, SalesIntegrationStub>();
        
        return services;
    }
}
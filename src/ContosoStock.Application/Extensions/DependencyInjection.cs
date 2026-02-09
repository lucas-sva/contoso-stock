using ContosoStock.Application.Fulfillment.UseCases.ReserveStock;
using ContosoStock.Domain.Fulfillment.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoStock.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Domain
        services.AddScoped<AllocationService>();
        
        // App handlers
        services.AddScoped<ReserveStockHandler>();
        
        return services;
    }
}
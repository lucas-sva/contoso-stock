using ContosoStock.Domain.Fulfillment.Ports.Contracts;
using ContosoStock.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoStock.Domain.Tests.Fulfillment.Integration;

public class StockLotIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceScope _scope;
    private readonly IStockLotRepository _repository;
    private readonly ContosoStockDbContext _context;

    public StockLotIntegrationTests(CustomWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _repository = _scope.ServiceProvider.GetRequiredService<IStockLotRepository>();
        _context = _scope.ServiceProvider.GetRequiredService<ContosoStockDbContext>();
    }
}
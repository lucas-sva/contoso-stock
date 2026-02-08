using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Infrastructure.Persistence.Contexts;
using ContosoStock.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContosoStock.Infrastructure.Tests.Persistence.Repositories;

public class StockLotRepositoryTests
{
    private readonly StockLotRepository _repository;
    private readonly ContosoStockDbContext _dbContext;
    public StockLotRepositoryTests()
    {
        // EF in memory
        var options = new DbContextOptionsBuilder<ContosoStockDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _dbContext = new ContosoStockDbContext(options);
        _repository = new StockLotRepository(_dbContext);
    }

    [Fact]
    public async Task AddAsync_DeveSalvarOLote_E_GetByIdAsync_DeveRecuperar()
    {
        // Arrange
        var sku = new Sku("TESTE-123");
        var zip = new ZipCode("60000-000");
        var id = Guid.NewGuid();
        
        var lot = new StockLot(id, sku, zip, 10, DateTime.Now.AddDays(30), false);
        
        // Act
        await _repository.AddAsync(lot);
        await _dbContext.SaveChangesAsync();
        
        // Assert
        var retrievedLot = await _repository.GetByIdAsync(id);
        
        Assert.NotNull(retrievedLot);
        Assert.Equal(sku.ToString(), retrievedLot.Sku.ToString());
        Assert.Equal(10, retrievedLot.Quantity);
    }

    [Fact]
    public async Task GetBySkuAsync_DeveRetornarApenasLotesDoSkuEspecificado()
    {
        // Arrange
        var skuAlvo = new Sku("TARGET-001");
        var skuOutro = new Sku("OTHER-999");
        var zip = new ZipCode("60000-000");
        
        var lot1 = new StockLot(Guid.NewGuid(), skuAlvo, zip, 5, DateTime.Now.AddDays(10), false);
        var lot2 = new StockLot(Guid.NewGuid(), skuAlvo, zip, 5, DateTime.Now.AddDays(10), false);
        var lot3 = new StockLot(Guid.NewGuid(), skuOutro, zip, 5, DateTime.Now.AddDays(10), false);
        
        await _repository.AddAsync(lot1);
        await _repository.AddAsync(lot2);
        await _repository.AddAsync(lot3);
        await _dbContext.SaveChangesAsync();
        
        // Act
        var result = await _repository.GetBySkuAsync(skuAlvo);
        var stockLots = result.ToList();
        
        // Assert
        Assert.Equal(2, stockLots.Count);
        Assert.All(stockLots, lot => Assert.Equal("TARGET-001",  lot.Sku.ToString()));
    }
}
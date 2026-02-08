using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.Ports.Contracts;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ContosoStock.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementação do Repositório de Lotes utilizando EF Core.
/// </summary>
public class StockLotRepository(ContosoStockDbContext dbContext) : IStockLotRepository
{
    public async Task AddAsync(StockLot lot) => await dbContext.StockLots.AddAsync(lot);

    public Task UpdateAsync(StockLot lot)
    {
        dbContext.StockLots.Update(lot);
        return Task.CompletedTask;
    }

    public async Task<StockLot?> GetByIdAsync(Guid id)
    {
        return await dbContext.StockLots.FindAsync(id);
    }

    public async Task<IEnumerable<StockLot>> GetBySkuAsync(Sku sku)
    {
        return await dbContext.StockLots
            .Where(lot => lot.Sku == sku)
            .ToListAsync();
    }
}
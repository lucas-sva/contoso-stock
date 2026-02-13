using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.ValueObjects;

namespace ContosoStock.Domain.Fulfillment.Repositories;

public interface IStockLotRepository
{
    Task AddAsync(StockLot lot,  CancellationToken cancellationToken = default);
    Task UpdateAsync(StockLot lot,  CancellationToken cancellationToken = default);
    Task<StockLot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<StockLot>> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default);
}
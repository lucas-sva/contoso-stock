using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.ValueObjects;

namespace ContosoStock.Domain.Fulfillment.Ports.Contracts;

public interface IStockLotRepository
{
    Task AddAsync(StockLot lot);
    Task UpdateAsync(StockLot lot);
    Task<StockLot?> GetByIdAsync(Guid id);
    Task<IEnumerable<StockLot>> GetBySkuAsync(Sku sku);
}
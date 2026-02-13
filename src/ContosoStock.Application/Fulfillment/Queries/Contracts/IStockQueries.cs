using ContosoStock.Application.Fulfillment.Queries.GetStockBySku;

namespace ContosoStock.Application.Fulfillment.Queries.Contracts;

public interface IStockQueries
{
    Task<IEnumerable<StockListItem>> GetBySkuAsync(string sku);
}
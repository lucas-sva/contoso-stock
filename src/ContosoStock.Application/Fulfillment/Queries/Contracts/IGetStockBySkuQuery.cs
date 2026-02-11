using ContosoStock.Application.Fulfillment.Queries.Dtos;

namespace ContosoStock.Application.Fulfillment.Queries.Contracts;

public interface IGetStockBySkuQuery
{
    Task<IEnumerable<StockListItem>> ExecuteAsync(string sku);
}
using MediatR;

namespace ContosoStock.Application.Fulfillment.Queries.GetStockBySku;

// ISSO é a mensagem que o Controller manda
public record GetStockBySkuQuery(string Sku) : IRequest<IEnumerable<StockListItem>>;
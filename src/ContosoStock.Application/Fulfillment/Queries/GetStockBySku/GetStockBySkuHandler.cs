using ContosoStock.Application.Fulfillment.Queries.Contracts;
using MediatR;

namespace ContosoStock.Application.Fulfillment.Queries.GetStockBySku;

public class GetStockBySkuHandler(IStockQueries queries) : IRequestHandler<GetStockBySkuQuery, IEnumerable<StockListItem>>
{
    private readonly IStockQueries _queries = queries;
    public async Task<IEnumerable<StockListItem>> Handle(GetStockBySkuQuery request, CancellationToken cancellationToken)
    {
        return await _queries.GetBySkuAsync(request.Sku);
    }
}
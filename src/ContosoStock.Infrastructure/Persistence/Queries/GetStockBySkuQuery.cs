using System.Data;
using ContosoStock.Application.Fulfillment.Queries.Contracts;
using ContosoStock.Application.Fulfillment.Queries.Dtos;
using Dapper;

namespace ContosoStock.Infrastructure.Persistence.Queries;

public class GetStockBySkuQuery(IDbConnection connection) : IGetStockBySkuQuery
{
    private readonly IDbConnection _connection = connection;

    public async Task<IEnumerable<StockListItem>> ExecuteAsync(string sku)
    {
        const string sql = """
                           SELECT 
                                "Sku", 
                                "Quantity", 
                                "ZipCode", 
                                "ExpirationDate"
                           FROM "StockLots"
                           WHERE "Sku" = @Sku
                           """;

        return await _connection.QueryAsync<StockListItem>(sql, new { Sku = sku });
    }
}
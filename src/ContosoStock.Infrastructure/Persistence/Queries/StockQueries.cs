using System.Data;
using ContosoStock.Application.Fulfillment.Queries.Contracts;
using ContosoStock.Application.Fulfillment.Queries.GetStockBySku;
using Dapper;

namespace ContosoStock.Infrastructure.Persistence.Queries;

public class StockQueries(IDbConnection connection) : IStockQueries
{
    public async Task<IEnumerable<StockListItem>> GetBySkuAsync(string sku)
    {
        const string sql = """
                           SELECT 
                                "Id" as "LotId", -- Mapeando para o DTO novo
                                "Sku", 
                                "Quantity", 
                                "ZipCode", 
                                "ExpirationDate"
                           FROM "StockLots"
                           WHERE "Sku" = @Sku
                           """;

        return await connection.QueryAsync<StockListItem>(sql, new { Sku = sku });
    }
}
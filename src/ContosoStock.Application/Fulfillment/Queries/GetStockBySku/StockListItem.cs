namespace ContosoStock.Application.Fulfillment.Queries.GetStockBySku;

public record StockListItem(
    Guid LotId,
    string Sku,
    int Quantity,
    string ZipCode,
    DateTime ExpirationDate,
    bool IsFragile
    );
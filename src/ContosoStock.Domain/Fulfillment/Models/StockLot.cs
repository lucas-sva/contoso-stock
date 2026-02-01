namespace ContosoStock.Domain.Fulfillment.Models;

public record StockLot (
    string Id,
    string Sku,
    int Quantity,
    DateTime ExpirationDate,
    bool IsFragile
);
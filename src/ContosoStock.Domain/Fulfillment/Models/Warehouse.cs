namespace ContosoStock.Domain.Fulfillment.Models;

public record DistributionCenter(
    string Id,
    string Name,
    string Region,
    bool IsActive
);

public record StockLot (
    string Id,
    string Sku,
    int Quantity,
    DateTime ExpirationDate,
    bool IsFragile
);
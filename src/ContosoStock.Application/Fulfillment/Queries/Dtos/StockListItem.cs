namespace ContosoStock.Application.Fulfillment.Queries.Dtos;

public record StockListItem(
    string Sku,
    int Quantity,
    string ZipCode,
    DateTime ExpirationDate
    );
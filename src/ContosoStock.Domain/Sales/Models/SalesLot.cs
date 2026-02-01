namespace ContosoStock.Domain.Sales.Models;

public record SalesLot(
    string Id,
    string CampaignName,
    decimal Price,
    decimal CommissionRate
);
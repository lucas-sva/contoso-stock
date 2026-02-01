namespace ContosoStock.Domain.Fulfillment.Models;

public record Warehouse (
    string Id,
    string Name,
    string Region,
    bool IsActive
);
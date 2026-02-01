namespace ContosoStock.Domain.Fulfillment.Models;

public record DistributionCenter(
    string Id,
    string Name,
    string Region,
    bool IsActive
);
using ContosoStock.Domain.Fulfillment.Models;

namespace ContosoStock.Domain.Fulfillment.Services;

public class AllocationService
{
    public string DeterminateBestWarehouse(string sku, string zipCode, IEnumerable<Warehouse> availableWarehouses)
    {
        var warehouse = availableWarehouses
        .FirstOrDefault(wh => wh.IsActive && zipCode.StartsWith(GetRegionPrefix(wh.Region)));
        return warehouse?.Id ?? "CENTRAL-DISTRIBUTION-01";
    }

    private static string GetRegionPrefix(string region) => region == "Southeast" ? "01" : "99";
}
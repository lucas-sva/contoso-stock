using ContosoStock.Domain.Fulfillment.Models;

namespace ContosoStock.Domain.Fulfillment.Services;

public static class AllocationService
{
    public static string Allocate(string sku, string zipCode, IEnumerable<DistributionCenter> cds)
    {
        var selectCd = cds.FirstOrDefault(cd => cd.IsActive);
        return selectCd?.Id ?? "CD-MATRIZ";
    }

    public static bool ReserveLot(StockLot lot, int quantityRequested)
    {
        if (lot.Quantity < quantityRequested) return false;
        Console.WriteLine($"[log] Lote {lot.Id} reservado com sucesso");    
        return true;
    }
}
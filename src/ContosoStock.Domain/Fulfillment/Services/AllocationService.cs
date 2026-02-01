using ContosoStock.Domain.Fulfillment.ACL;
using ContosoStock.Domain.Fulfillment.Models;

namespace ContosoStock.Domain.Fulfillment.Services;

public class AllocationService(ISalesIntegration salesIntegration)
{
    private readonly ISalesIntegration _salesIntegration = salesIntegration;

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

    public void ProcessOrderFulfillment(string saleId, string sku, int quantity, IEnumerable<DistributionCenter> cds)
    {
        var targetCd = Allocate(sku, "01000", cds);
        
        var isAuthorized = _salesIntegration.RequestStockReservation(saleId, sku, quantity, targetCd);
        
        if (isAuthorized)
            Console.WriteLine($"[log] Alocação confirmada para o pedido {saleId}");
    }
}
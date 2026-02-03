using ContosoStock.Domain.Fulfillment.ACL;
using ContosoStock.Domain.Fulfillment.Models;

namespace ContosoStock.Domain.Fulfillment.Services;

public class AllocationService(ISalesIntegration salesIntegration)
{
    private readonly ISalesIntegration _salesIntegration = salesIntegration;

    public static string Allocate(IEnumerable<DistributionCenter> cds, ZipCode zipCode)
    {
        var selectCd = cds.FirstOrDefault(cd => cd.IsActive);
        return selectCd?.Id ?? "CD-MATRIZ";
    }

    public void ProcessOrderFulfillment(string saleId, StockLot lot, IEnumerable<DistributionCenter> cds, ZipCode zipCode, int quantity)
    {
        var targetCd = Allocate(cds, zipCode);
        
        if(!lot.Reserve(quantity))
            throw new InvalidOperationException($"Não foi possível reservar o lote {lot.Id}");
        
        var isAuthorized = _salesIntegration.RequestStockReservation(saleId, lot.Id, targetCd, quantity);
        
        if (isAuthorized)
            Console.WriteLine($"[log] Alocação confirmada para o pedido {saleId}");
    }
}

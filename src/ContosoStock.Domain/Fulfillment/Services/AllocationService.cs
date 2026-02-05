using ContosoStock.Domain.Fulfillment.ACL;
using ContosoStock.Domain.Fulfillment.Models;
using ZipCode = ContosoStock.Domain.Fulfillment.ValueObjects.ZipCode;

namespace ContosoStock.Domain.Fulfillment.Services;

/// <summary>
/// Serviço de Domínio responsável por coordenar o processo de alocação de pedidos,
/// interagindo com múltiplos Centros de Distribuição e validando a reserva nos lotes.
/// </summary>
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
        
        var reserveResult = lot.Reserve(quantity);
        
        if(reserveResult.IsFailure)
            throw new InvalidOperationException(reserveResult.Error);
        
        var isAuthorized = _salesIntegration.RequestStockReservation(saleId, lot.Id, targetCd, quantity);

        if (!isAuthorized)
        {
            lot.Release(quantity);
            throw new InvalidOperationException($"Venda negada para o pedido {saleId}. Estoque estornado.");
        }
        
        Console.WriteLine($"[log] Alocação e Reserva confirmadas com sucesso: Pedido {saleId}");
    }
}

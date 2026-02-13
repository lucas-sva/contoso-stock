using ContosoStock.Domain.Fulfillment.Gateways;
using ContosoStock.Domain.Fulfillment.Models;
using ZipCode = ContosoStock.Domain.Fulfillment.ValueObjects.ZipCode;

namespace ContosoStock.Domain.Fulfillment.Services;

/// <summary>
/// Serviço de Domínio responsável por coordenar o processo de alocação de pedidos,
/// interagindo com múltiplos Centros de Distribuição e validando a reserva nos lotes.
/// </summary>
public class AllocationService(ISalesGateway salesIntegration)
{
    /// <summary>
    /// Executa a Reserva de Alocação Otimizada (RAO)
    /// </summary>
    public StockLot? ExecuteRao(
        ZipCode destinationZip, 
        IEnumerable<DistributionCenter> availableCds, 
        IEnumerable<StockLot> availableLots, 
        int requiredQuantity)
    {
        var eligibleCds = availableCds
            .Where(cd => cd.CanFulfill(destinationZip))
            .Select(cd => cd.Id)
            .ToHashSet();

        if (eligibleCds.Count == 0)
            return null;

        return availableLots
            .Where(lot => eligibleCds.Contains(lot.DistributionCenterId)) // Está num CD correto?
            .Where(lot => lot.Quantity >= requiredQuantity)               // Tem saldo?
            .Where(lot => lot.ExpirationDate > DateTime.UtcNow)           // Não venceu?
            .OrderBy(lot => lot.ExpirationDate)                           // FEFO: Vence primeiro sai primeiro
            .FirstOrDefault();                                            // Pega o melhor
    }
}

using ContosoStock.Domain.Fulfillment.Ports.ACL;

namespace ContosoStock.Infrastructure.Adapters.Sales;

/// <summary>
/// Stub para simular a aprovação de vendas
/// enquanto o serviço real HTTP não é implementado.
/// </summary>
public class SalesIntegrationStub : ISalesIntegration
{
    public bool RequestStockReservation(string saleId, Guid lotId, string distributionCenterId, int quantity)
    {
        return quantity <= 1000;
    }
}
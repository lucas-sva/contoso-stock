using ContosoStock.Domain.Fulfillment.Gateways;

namespace ContosoStock.Infrastructure.ExternalServices.Sales;

/// <summary>
/// Stub para simular a aprovação de vendas
/// enquanto o serviço real HTTP não é implementado.
/// </summary>
public class SalesIntegrationStub : ISalesGateway
{
    public bool RequestStockReservation(string saleId, Guid lotId, string distributionCenterId, int quantity)
    {
        return quantity <= 1000;
    }
}
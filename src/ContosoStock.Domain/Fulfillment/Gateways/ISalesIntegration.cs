namespace ContosoStock.Domain.Fulfillment.Gateways;

/// <summary>
/// ACL (Anticorruption Layer) para o Contexto de Vendas.
/// Protege o Fulfillment de mudanças no modelo comercial.
/// </summary>
public interface ISalesGateway
{
    bool RequestStockReservation(string saleId, Guid lotId, string distributionCenterId, int quantity);
}
namespace ContosoStock.Domain.Fulfillment.Ports.ACL;

/// <summary>
/// ACL (Anticorruption Layer) para o Contexto de Vendas.
/// Protege o Fulfillment de mudanças no modelo comercial.
/// </summary>
public interface ISalesIntegration
{
    bool RequestStockReservation(string saleId, Guid lotId, string distributionCenterId, int quantity);
}
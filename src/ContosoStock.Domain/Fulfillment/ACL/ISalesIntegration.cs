using ContosoStock.Domain.Fulfillment.Models;

namespace ContosoStock.Domain.Fulfillment.ACL;

/// <summary>
/// ACL (Anticorruption Layer) para o Contexto de Vendas.
/// Protege o Fulfillment de mudanças no modelo comercial.
/// </summary>
public interface ISalesIntegration
{
    bool RequestStockReservation(string saleId, string lotId, string distributionCenterId, int quantity);
}
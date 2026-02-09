namespace ContosoStock.Application.Fulfillment.UseCases.ReserveStock;

/// <summary>
/// DTO de Entrada: Representa a intenção de reservar estoque.
/// Não contém lógica, apenas dados primitivos.
/// </summary>
public record ReserveStockCommand(
    string SaleId,
    string Sku,
    int Quantity,
    string ZipCode
    );
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.Helpers;
using MediatR;

namespace ContosoStock.Application.Fulfillment.UseCases.ReserveStock;

/// <summary>
/// DTO de Entrada: Representa a intenção de reservar estoque.
/// Não contém lógica, apenas dados primitivos.
/// Att com MediatR + Event Sourcing
/// </summary>
public record ReserveStockCommand(
    string Sku,
    string DestinationZipCode,
    int Quantity,
    bool HandleFragile
    ) : IRequest<Result>;
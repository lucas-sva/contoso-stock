using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

namespace ContosoStock.Domain.Fulfillment.Events;

public record StockReservedEvent(Guid LotId, Sku Sku, int Quantity) : IDomainEvent
{
    public DateTime OccurredOn { get; } =  DateTime.UtcNow;
}
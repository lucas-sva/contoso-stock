using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

namespace ContosoStock.Domain.Fulfillment.Events;

public record StockReleasedEvent(Guid LotId, int Quantity) : IDomainEvent
{
    public Guid AggregateId => LotId;
    public DateTime OccurredOn { get; init; } =  DateTime.UtcNow;
}
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

namespace ContosoStock.Domain.Fulfillment.Events;

public record StockLotCreatedEvent(
    Guid LotId, 
    Guid DistributionCenterId,
    string Sku,
    int Quantity,
    DateTime ExpirationDate,
    bool IsFragile = false) : IDomainEvent
{
    public Guid AggregateId => LotId;
    public DateTime OccurredOn { get; init; }  = DateTime.UtcNow;
}
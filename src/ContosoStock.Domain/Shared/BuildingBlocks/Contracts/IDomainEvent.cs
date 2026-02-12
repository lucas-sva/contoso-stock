namespace ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

public interface IDomainEvent
{
    Guid AggregateId { get; }
    DateTime OccurredOn { get; }
}
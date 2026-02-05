namespace ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
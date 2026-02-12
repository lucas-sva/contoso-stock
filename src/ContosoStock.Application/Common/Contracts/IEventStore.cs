using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

namespace ContosoStock.Application.Common.Contracts;

public interface IEventStore
{
    Task AppendEventsAsync(Guid aggregateId, IEnumerable<IDomainEvent> events, long expectedVersion, CancellationToken cancellationToken = default);
    Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default);
}
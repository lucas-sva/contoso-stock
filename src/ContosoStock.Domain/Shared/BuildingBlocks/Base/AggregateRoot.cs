using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

namespace ContosoStock.Domain.Shared.BuildingBlocks.Base;

public abstract class AggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    
    public void ClearDomainEvents() => _domainEvents.Clear();
}
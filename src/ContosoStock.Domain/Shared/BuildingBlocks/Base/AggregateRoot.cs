using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

namespace ContosoStock.Domain.Shared.BuildingBlocks.Base;

public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }
    private readonly List<IDomainEvent> _changes = [];
    
    public long Version { get; private set; } = -1;
    
    public IEnumerable<IDomainEvent> GetUncommittedChanges() => _changes.AsReadOnly();
    
    public void MarkChangesAsCommitted() => _changes.Clear();
    
    protected void RaiseEvent(IDomainEvent @event) => ApplyEvent(@event, true);
    
    private void ApplyEvent(IDomainEvent @event, bool isNew)
    {
        ((dynamic)this).Apply((dynamic)@event);

        if (isNew)
            _changes.Add(@event);
    }
    
    public void LoadFromHistory(IEnumerable<IDomainEvent> history)
    {
        foreach (var @event in history)
        {
            ApplyEvent(@event, false);
            Version++;
        }
    }
}
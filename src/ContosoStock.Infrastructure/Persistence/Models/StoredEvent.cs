namespace ContosoStock.Infrastructure.Persistence.Models;

public class StoredEvent
{
    public Guid Id { get; private set; }
    public Guid AggregateId { get; private set; }
    public string? MessageType { get; private set; }
    public string? Data { get; private set; }
    public long Version { get; private set; }
    public DateTime OccurredOn { get; private set; }
    
    protected StoredEvent() { } // Ctor pro EF
    
    public StoredEvent(Guid aggregateId, string messageType, string data, long version)
    {
        Id = Guid.NewGuid();
        AggregateId = aggregateId;
        MessageType = messageType;
        Data = data;
        Version = version;
        OccurredOn = DateTime.UtcNow;
    }
}
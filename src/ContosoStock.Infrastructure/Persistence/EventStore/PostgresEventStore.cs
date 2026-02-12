using System.Text.Json;
using ContosoStock.Application.Common.Contracts;
using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;
using ContosoStock.Infrastructure.Persistence.Contexts;
using ContosoStock.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoStock.Infrastructure.Persistence.EventStore;

public class PostgresEventStore(ContosoStockDbContext context) : IEventStore
{
    private readonly ContosoStockDbContext _context = context;

    public async Task AppendEventsAsync(Guid aggregateId, IEnumerable<IDomainEvent> events, long expectedVersion,
        CancellationToken cancellationToken = default)
    {
        // Verificação de ocorrência otimista. Se alguém já salvou eventos com versão superior a que eu esperava, falha.
        // TODO: implementar isso na próx interação
        
        var currentVersion = expectedVersion;

        foreach (var @event in events)
        {
            currentVersion++;
            
            var eventType = @event.GetType().AssemblyQualifiedName;
            var jsonData = JsonSerializer.Serialize(@event, @event.GetType());

            var storedEvent = new StoredEvent(
                aggregateId,
                eventType!,
                jsonData,
                currentVersion
            );
            
            _context.Events.Add(storedEvent);
        }
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        var storedEvents = await _context.Events
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.Version)
            .ToListAsync(cancellationToken);

        var domainEvents = new List<IDomainEvent>();

        foreach (var storedEvent in storedEvents)
        {
            if (storedEvent.MessageType == null || storedEvent.Data == null) continue;
            
            var type = Type.GetType(storedEvent.MessageType);

            if (type == null) continue;
            
            if (JsonSerializer.Deserialize(storedEvent.Data, type) is IDomainEvent domainEvent)
                domainEvents.Add(domainEvent);
        }
        
        return domainEvents;
    }
}
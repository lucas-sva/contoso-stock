using ContosoStock.Application.Common.Contracts;
using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.Ports.Contracts;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;
using ContosoStock.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ContosoStock.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementação do Repositório de Lotes utilizando EF Core.
/// Atualiizado para Data Sourcing
/// </summary>
public class StockLotRepository(ContosoStockDbContext context, IEventStore eventStore) : IStockLotRepository
{
    private readonly ContosoStockDbContext _dbContext = context;
    private readonly IEventStore _eventStore = eventStore;

    public async Task AddAsync(StockLot lot, CancellationToken cancellationToken = default)
    {
        await _eventStore.AppendEventsAsync(
            lot.Id, 
            lot.GetUncommittedChanges(), 
            lot.Version, 
            cancellationToken);
        
        _dbContext.StockLots.Add(lot);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        // Queue clear
        lot.MarkChangesAsCommitted();
    }

    public async Task UpdateAsync(StockLot lot, CancellationToken cancellationToken = default)
    {
        await _eventStore.AppendEventsAsync(
            lot.Id, 
            lot.GetUncommittedChanges(), 
            lot.Version, 
            cancellationToken);
        
        _dbContext.StockLots.Update(lot);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        lot.MarkChangesAsCommitted();
    }

    public async Task<StockLot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // Rehidratar via EventStore (maior segurança, menor responsividade)
        var events = await _eventStore.GetEventsAsync(id, cancellationToken);
        var domainEvents = events as IDomainEvent[] ?? events.ToArray();
        if (domainEvents.Length == 0) return null;
        
        var lot = new StockLot();
        lot.LoadFromHistory(domainEvents);
        
        return lot;
    }

    public async Task<IEnumerable<StockLot>> GetBySkuAsync(Sku sku, CancellationToken cancellationToken = default)
    {
        return await _dbContext.StockLots
            .Where(lot => lot.Sku == sku)
            .ToListAsync(cancellationToken);
    }
}
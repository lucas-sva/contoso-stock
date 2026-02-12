using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.Ports.Contracts;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Infrastructure.Persistence.Contexts;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace ContosoStock.Api.Tests.Integration;

public class StockLotIntegrationTests(CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly ITestOutputHelper _testOutputHelper =  testOutputHelper;
    private readonly CustomWebApplicationFactory _factory = factory;

    /// <summary>
    /// Hybrid Persistence Validation
    /// Valida o ciclo de vida completo de um AggregateRoot:
    /// 1. Persistência do estado atual (Snapshot/Read Model) na tabela StockLots.
    /// 2. Persistência do histórico de eventos (Append-only) na tabela Events em formato JSONB.
    /// 3. Garantia de atomicidade da transação via Unit of Work.
    /// </summary>
    [Fact]
    public async Task Should_Persist_Both_State_And_Events_In_Database()
    {
        using var scope = _factory.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IStockLotRepository>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ContosoStockDbContext>();
        
        // Arrange
        var lotId = Guid.NewGuid();
        var lot = new StockLot(lotId, new Sku("TECLADO-MECH"), new ZipCode("63000-000"), 50,
            DateTime.UtcNow.AddDays(30), true);
        lot.Reserve(10, true);
        
        // Act
        await repository.AddAsync(lot);
        
        // DEBUG: Verifique se o EF Core acha que salvou algo
        var totalEventsNoBanco = await dbContext.Events.CountAsync();
        _testOutputHelper.WriteLine($"Total de eventos no banco: {totalEventsNoBanco}");

        var eventsDoLote = await dbContext.Events.Where(x => x.AggregateId == lotId).ToListAsync();
        _testOutputHelper.WriteLine($"Eventos para o lote {lotId}: {eventsDoLote.Count}");
        
        var stateEntry = await dbContext.StockLots
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == lot.Id);

        var events = await dbContext.Events
            .Where(x => x.AggregateId == lotId)
            .OrderBy(x => x.Version)
            .ToListAsync();
        
        // Assert
        stateEntry.Should().NotBeNull();
        stateEntry!.Quantity.Should().Be(40);
        events.Should().HaveCount(2);
        events[0].MessageType.Should().Contain("StockLotCreatedEvent");
        events[1].MessageType.Should().Contain("StockReservedEvent");
        events[1].Data.Should().Contain("\"Quantity\":10");
    }
}
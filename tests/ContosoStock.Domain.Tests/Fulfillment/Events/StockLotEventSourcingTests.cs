using ContosoStock.Domain.Fulfillment.Events;
using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;
using FluentAssertions;

namespace ContosoStock.Domain.Tests.Fulfillment.Events;

public class StockLotEventSourcingTests
{
    /// <summary>
    /// Rehydratable
    /// Tenta recriar um StockLot a partir de um lot vazio e uma lista de eventos passados 
    /// </summary>
    [Fact]
    public void Should_Rehydrate_State_Correctly_From_History()
    {
        // Arrange
        var lotId =  Guid.NewGuid();
        var sku = new Sku("MOUSE-GAMER-123");
        var zipCode = new ZipCode("60525-000");
        var history = new List<IDomainEvent>
        {
            new StockLotCreatedEvent(lotId, sku, zipCode, 100, DateTime.UtcNow.AddDays(30)),
            new StockReservedEvent(lotId, 10),
            new StockReservedEvent(lotId, 20),
            new StockReleasedEvent(lotId, 10)
        };
        
        // Act
        var lot = new StockLot();
        lot.LoadFromHistory(history);
        
        // Assert
        lot.Quantity.Should().Be(80);
        lot.Sku.Should().Be(sku);
        lot.ZipCode.Should().Be(zipCode);
        lot.Version.Should().Be(3);
    }
}
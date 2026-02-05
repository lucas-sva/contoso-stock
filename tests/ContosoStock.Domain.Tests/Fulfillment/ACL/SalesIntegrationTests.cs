using ContosoStock.Domain.Fulfillment.ACL;
using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.Services;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using NSubstitute;

namespace ContosoStock.Domain.Tests.Fulfillment.ACL;

public class SalesIntegrationTests
{
    [Fact]
    public void ProcessOrderFulfillment_DeveFinalizar_QuandoVendaForAutorizada()
    {
        // Arrange
        var lotId = Guid.NewGuid();
        var sku = new Sku("GEL-001");
        var zip = new ZipCode("60525-000");
        
        var mockSales = Substitute.For<ISalesIntegration>();
        var sut = new AllocationService(mockSales);
        var cds = new List<DistributionCenter> { new("CD-01", "Matriz", "Southeast", true) };
        var lot = new StockLot(lotId, sku, zip, 10, DateTime.Now.AddDays(10), false);
        
        mockSales
            .RequestStockReservation(
                Arg.Any<string>(), 
                 Arg.Any<Guid>(), 
                Arg.Any<string>(), 
                Arg.Any<int>())
            .Returns(true);
        
        // Act
        sut.ProcessOrderFulfillment("VENDA-123", lot, cds, zip, 5);
        
        // Assert
        mockSales
            .Received(1)
            .RequestStockReservation("VENDA-123", lot.Id, cds[0].Id, 5);
    }
}
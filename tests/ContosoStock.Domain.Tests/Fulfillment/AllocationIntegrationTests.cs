using ContosoStock.Domain.Fulfillment.ACL;
using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.Services;
using NSubstitute;

namespace ContosoStock.Domain.Tests.Fulfillment;

public class AllocationIntegrationTests
{
    [Fact]
    public void ProcessOrderFulfillment_DeveFinalizar_QuandoVendaForAutorizada()
    {
        // Arrange
        var mockSales = Substitute.For<ISalesIntegration>();
        var sut = new AllocationService(mockSales);
        var cds = new List<DistributionCenter> { new("CD-01", "Matriz", "Southeast", true) };
        var lot = new StockLot(Guid.Empty, new Sku("001"), new ZipCode("60525-000"), 10, DateTime.Now.AddDays(10), false);
        
        mockSales
            .RequestStockReservation(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<int>())
            .Returns(true);
        
        // Act
        sut.ProcessOrderFulfillment("001", lot, cds, lot.ZipCode, 5);
        
        // Assert
        mockSales.Received(1).RequestStockReservation("001", lot.Id, cds[0].Id, 5);
    }
}
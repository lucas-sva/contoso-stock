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
        var lot = new StockLot("LOT-01", "SKU-99", 10, DateTime.Now.AddDays(10), false);
        
        mockSales
            .RequestStockReservation(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>())
            .Returns(true);
        
        // Act
        sut.ProcessOrderFulfillment("VENDA-123", lot, cds, 5);
        
        // Assert
        mockSales.Received(1).RequestStockReservation("VENDA-123", lot.Id, "CD-01", 5);
    }
}
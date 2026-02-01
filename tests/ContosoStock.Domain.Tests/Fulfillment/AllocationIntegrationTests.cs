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
        mockSales
            .RequestStockReservation(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<string>())
            .Returns(true);
        
        // Act
        sut.ProcessOrderFulfillment("VENDA-123", "SKU-99", 5, cds);
        
        // Assert
        mockSales.Received(1).RequestStockReservation("VENDA-123", "SKU-99", 5, "CD-01");
    }
}
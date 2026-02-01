using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.Services;

namespace ContosoStock.Domain.Tests.Fulfillment;

public class AllocationServiceTests
{
    [Fact]
    public void Allocate_ShouldReturnCdEast_WhenZipCodeStartsWith01()
    {
        // Arrange
        const string sku = "GELADEIRA-LUXO-01";
        const string zipCode = "01000-000";
        var cds = new List<DistributionCenter>
        {
            new("CD-SP-01", "CD São Paulo", "Southeast", true),
            new("CD-BA-01", "CD Bahia", "Northeast", true)
        };
        
        // Act
        var result = AllocationService.Allocate(sku, zipCode, cds);
        
        // Assert
        Assert.Equal("CD-SP-01", result);
    }

    [Fact]
    public void ReserveLot_ShouldReturnTrue_WhenQuantityIsAvailable()
    {
        // Arrange
        var lot = new StockLot(
            "LOT-2026",
            "SKU-99",
            100,
            DateTime.Now.AddDays(10),
            false);
        
        // Act
        var result = AllocationService.ReserveLot(lot, 50);
        
        // Assert
        Assert.True(result);
    }
}
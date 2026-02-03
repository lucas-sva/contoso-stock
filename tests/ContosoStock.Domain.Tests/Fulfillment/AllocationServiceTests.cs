using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.Services;

namespace ContosoStock.Domain.Tests.Fulfillment;

public class AllocationServiceTests
{
    [Fact]
    public void Allocate_ShouldReturnCdEast_WhenZipCodeStartsWith01()
    {
        // Arrange
        var cds = new List<DistributionCenter>
        {
            new("CD-SP-01", "CD São Paulo", "Southeast", true),
            new("CD-BA-01", "CD Bahia", "Northeast", true)
        };
        
        // Act
        var result = AllocationService.Allocate(cds, new ZipCode("01525-000"));
        
        // Assert
        Assert.Equal("CD-SP-01", result);
    }

    [Fact]
    public void ReserveLot_ShouldReturnTrue_WhenQuantityIsAvailable()
    {
        // Arrange
        var lot = new StockLot(Guid.NewGuid(), new Sku("0001"), new ZipCode("01525-000"), 100, DateTime.Now.AddDays(10), false);
        
        // Act
        var result = lot.Reserve(50);
        
        // Assert
        Assert.True(result);
    }
}
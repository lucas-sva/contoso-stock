using ContosoStock.Domain.Sales.Models;

namespace ContosoStock.Domain.Tests.Sales;

public class SalesLotTests
{
    [Fact]
    public void SalesLot_ShouldCalculateCommission_Correctly()
    {
        // Arrange
        var lot = new SalesLot(
            "PROMO-NATAL",
            "Natal 2026",
            1000m,
            0.10m);
        
        // Act
        var commission = lot.Price * lot.CommissionRate;
        
        // Assert
        Assert.Equal(100m, commission);
    }
}
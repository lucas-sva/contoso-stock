using ContosoStock.Domain.Fulfillment.Models;

namespace ContosoStock.Domain.Tests.Fulfillment;

public class StockLotTests
{
    [Fact]
    public void StockSlot_NaoDeveAlocar_SeEstiverVencido()
    {
        // Arrange
        var lote = new StockLot(Guid.NewGuid(), new Sku("001"), new ZipCode("01525-000"), 10, DateTime.Now.AddDays(-1), false);
        
        // Assert
        Assert.False(lote.Reserve(1));
    }
}
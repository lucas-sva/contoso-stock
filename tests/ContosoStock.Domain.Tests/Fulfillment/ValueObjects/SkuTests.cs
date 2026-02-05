using ContosoStock.Domain.Fulfillment.ValueObjects;

namespace ContosoStock.Domain.Tests.Fulfillment.ValueObjects;

public class SkuTests
{
    [Fact]
    public void Sku_DeveCriarInstancia_QuandoValorForValido()
    {
        // Arrange
        const string validCode = "GEL-123";
        
        // Act
        var sku = new Sku(validCode);
        
        // Assert
        Assert.Equal(validCode, sku.ToString());
    }
    
    [Fact]
    public void Sku_DeveConverterParaMaiusculo_Automaticamente()
    {
        // Act
        var sku = new Sku("abc-123");

        // Assert
        Assert.Equal("ABC-123", sku.ToString());
    }
}
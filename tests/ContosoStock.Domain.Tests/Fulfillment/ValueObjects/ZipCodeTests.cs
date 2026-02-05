using ContosoStock.Domain.Fulfillment.ValueObjects;

namespace ContosoStock.Domain.Tests.Fulfillment.ValueObjects;

public class ZipCodeTests
{
    [Fact]
    public void ZipCode_DeveCriarInstancia_QuandoFormatoCorreto()
    {
        // Act
        var zip = new ZipCode("60525-000");
        
        // Assert
        Assert.Equal("60525-000", zip.ToString());
    }

    [Theory]
    // Testando SP
    [InlineData("01000-000", "SP")]
    // Testando Ceará (Supondo faixa 60-63)
    [InlineData("60525-000", "CE")]
    public void GetRegionCode_DeveRetornarEstadoCorreto_BaseadoNosPrefixos(string cep, string estadoEsperado)
    {
        // Act
        var result = ZipCode.GetRegionCode(cep);
        
        // Assert
        Assert.Equal(estadoEsperado, result);
    }
}
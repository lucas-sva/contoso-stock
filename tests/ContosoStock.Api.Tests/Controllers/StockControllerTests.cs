using System.Net.Http.Json;
using ContosoStock.Application.Fulfillment.UseCases.ReserveStock;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ContosoStock.Api.Tests.Controllers;

public class StockControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task FluxoCompleto_Seed_E_Reserva_DeveFuncionar()
    {
        // Cria estoque inicial
        var seedResponse = await _client.PostAsync("/api/stock/seed", null);
        seedResponse.EnsureSuccessStatusCode();
        
        // Reserva estoque
        var command = new ReserveStockCommand(
            SaleId: "001",
            Sku:"GEL-123",
            Quantity: 10,
            ZipCode: "60525-000"
            );
        
        var reserveResponse = await _client.PostAsJsonAsync("api/stock/reserve", command);
        
        // Assert
        reserveResponse.EnsureSuccessStatusCode();

        var result = await reserveResponse.Content.ReadFromJsonAsync<ReserveStockResult>();
        Assert.True(result?.Success);
        Assert.NotNull(result?.ReservedLotId);
    }
    
    [Fact]
    public async Task Reserva_DeveRetornarBadRequest_SeSkuNaoExistir()
    {
        var command = new ReserveStockCommand("ORDER-000", "SKU-FANTASMA", 1, "00000-000");

        var response = await _client.PostAsJsonAsync("/api/stock/reserve", command);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }
}
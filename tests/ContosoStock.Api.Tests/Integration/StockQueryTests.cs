using ContosoStock.Application.Fulfillment.Queries.Contracts;
using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoStock.Api.Tests.Integration;

public class StockQueryTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task Deve_Ler_Dados_Via_Dapper_Que_Foram_Escritos_Via_EFCore()
    {
        // Arrange -> escrita via EF Core
        var uniqueSku = $"CQRS-{Guid.NewGuid().ToString()[..8]}".ToUpper();
        
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ContosoStockDbContext>();
            await dbContext.Database.EnsureCreatedAsync(); 

            var sku = new Sku(uniqueSku); 
            var lote = new StockLot(
                Guid.NewGuid(), 
                sku, 
                new ZipCode("12345-678"), 
                50, 
                DateTime.UtcNow.AddDays(10),
                false
            );

            dbContext.StockLots.Add(lote);
            await dbContext.SaveChangesAsync();
        }
        
        // Act -> Leitura via Dapper
        using (var scope = _factory.Services.CreateScope())
        {
            var query = scope.ServiceProvider.GetRequiredService<IGetStockBySkuQuery>();
            var resultado = await query.ExecuteAsync(uniqueSku);
            var lista = resultado.ToList(); // Materializa para testar

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(lista); // Deve encontrar 1 registro
            
            var item = lista.First();
            
            // Validar contra o 'uniqueSku'
            Assert.Equal(uniqueSku, item.Sku); 
            Assert.Equal(50, item.Quantity);
            Assert.Equal("12345-678", item.ZipCode);
        }
    }
}
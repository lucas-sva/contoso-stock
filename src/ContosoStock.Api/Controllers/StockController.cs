using ContosoStock.Application.Fulfillment.UseCases.ReserveStock;
using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace ContosoStock.Api.Controllers;

[ApiController]
[Route("api/stock")]
public class StockController(ReserveStockHandler handler, ContosoStockDbContext dbContext)
    : ControllerBase
{
    private readonly ReserveStockHandler _handler = handler;
    private readonly ContosoStockDbContext _dbContext = dbContext; // Seed

    /// <summary> Reserva estoque para um pedido de venda. </summary>
    /// <param name="command">Dados do pedido (SaleId, Sku, Qtd, CEP)</param>
    /// <param name="cancellationToken"> Unit of Works</param>
    /// <returns>Resultado da operação (200 OK ou 400 BadRequest)</returns>
    [HttpPost("reserve")]
    [ProducesResponseType(typeof(ReserveStockResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReserveStockResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Reserve([FromBody] ReserveStockCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _handler.Handle(command, cancellationToken);

        if (!result.Success)
            return BadRequest(result);
        
        return Ok(result);
    }

    /// <summary>
    /// Endpoint auxiliar para criar massa de dados (SEED) e permitir testes manuais.
    /// Em produção, isso seria feito por uma rotina de carga ou endpoint de administração.
    /// </summary>
    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        var sku = new Sku("GEL-123");
        var zip = new ZipCode("60000-000");
        var lotId = Guid.NewGuid();
        
        var lot = new StockLot(lotId,sku,zip,100, DateTime.UtcNow.AddDays(30), false);

        await _dbContext.StockLots.AddAsync(lot);
        await _dbContext.SaveChangesAsync();

        return Ok(new
        {
            Message = "Lote criado com sucesso!",
            Sku = sku,
            Cep = zip,
            Quantidade = lot.Quantity
        });
    }
}
using ContosoStock.Application.Fulfillment.Queries.GetStockBySku;
using ContosoStock.Application.Fulfillment.UseCases.ReserveStock;
using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.Repositories;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContosoStock.Api.Controllers;

[ApiController]
[Route("api/stock")]
public class StockController(IMediator mediator, IStockLotRepository repository) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IStockLotRepository _repository = repository;

    /// <summary>
    /// Reserva estoque para um pedido de venda (Command).
    /// </summary>
    [HttpPost("reserve")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Reserve([FromBody] ReserveStockCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Consulta rápida de estoque (Query).
    /// </summary>
    [HttpGet("{sku}")]
    public async Task<IActionResult> GetStock(string sku)
    {
        var query = new GetStockBySkuQuery(sku);
        
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }

    /// <summary>
    /// Endpoint auxiliar para criar massa de dados (SEED) e permitir testes manuais.
    /// Em produção, isso seria feito por uma rotina de carga ou endpoint de administração.
    /// </summary>
    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        var skuResult = Sku.Create("GEL-123");
        var zipResult = ZipCode.Create("60000-000");
        
        if (skuResult.IsFailure || zipResult.IsFailure)
            return BadRequest("Dados de seed inválidos");
        
        var lotId = Guid.NewGuid();
        var distributionCenterId = Guid.NewGuid();
        
        var lot = new StockLot(
            lotId,
            distributionCenterId,
            skuResult.Value,
            100, 
            DateTime.UtcNow.AddDays(30), 
            false
        );

        await _repository.AddAsync(lot);
        return Ok(new
        {
            Message = "Lote criado com sucesso (Event Sourcing Active)!",
            LotId = lotId,
            Sku = skuResult.Value.Value,
            Quantidade = lot.Quantity
        });
    }
}
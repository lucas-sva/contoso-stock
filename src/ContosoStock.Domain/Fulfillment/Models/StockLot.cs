using ContosoStock.Domain.Fulfillment.Events;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared;
using ContosoStock.Domain.Shared.BuildingBlocks.Base;
using ContosoStock.Domain.Shared.Helpers;

namespace ContosoStock.Domain.Fulfillment.Models;

/// <summary>
/// Aggregate Root que representa um lote físico de produtos.
/// Garante a invariante de que o saldo nunca seja negativo e que 
/// lotes vencidos não sejam reservados.
/// </summary>
public class StockLot(Guid id, Sku sku, ZipCode zipCode, int quantity, DateTime expirationDate, bool isFragile) : AggregateRoot
{
    public Guid Id { get; } = id;
    public Sku Sku { get; } = sku;
    public ZipCode ZipCode { get; } =  zipCode;
    private int Quantity { get; set; } = quantity;
    private DateTime ExpirationDate { get; } = expirationDate;
    private bool IsFragile { get; } = isFragile;
    private long Version { get; set; }

    public Result Reserve(int quantityRequested, bool handleFragile = false)
    {
        if (ExpirationDate <= DateTime.Now)
            return Result.Failure("Lote vencido");
        
        if (IsFragile && !handleFragile)
            return Result.Failure("Requer manuseio especial");

        if (Quantity < quantityRequested)
            return Result.Failure("Saldo insuficiente");

        Quantity -= quantityRequested;
        Version++;
        
        RaiseDomainEvent(new StockReservedEvent(Id, Sku, quantityRequested));
        
        return Result.Success();
    }

    public void Release(int quantityToRelease)
    {
        Quantity += quantityToRelease;
        Version++;
    }
}
using ContosoStock.Domain.Fulfillment.Events;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.BuildingBlocks.Base;
using ContosoStock.Domain.Shared.Helpers;

namespace ContosoStock.Domain.Fulfillment.Models;

/// <summary>
/// Aggregate Root que representa um lote físico de produtos.
/// Garante as invariantes.
/// Atualizado para Event sourcing
/// </summary>
public class StockLot : AggregateRoot
{
    public Guid Id { get; private set; }
    public Sku? Sku { get; private set; }
    public ZipCode? ZipCode { get; private set; }
    public int Quantity { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsFragile { get; private set; }
    
    public StockLot(Guid id, Sku sku, ZipCode zipCode, int quantity, DateTime expirationDate, bool isFragile = false)
    {
        RaiseEvent(new StockLotCreatedEvent(
            id, 
            sku, 
            zipCode, 
            quantity, 
            expirationDate, 
            isFragile));
    }

    public StockLot()
    {
        
    }
    public Result Reserve(int quantityRequested, bool handleFragile = false)
    {
        if (ExpirationDate <= DateTime.UtcNow)
            return Result.Failure("Lote vencido");
        
        if (IsFragile && !handleFragile)
            return Result.Failure("Requer manuseio especial");

        if (Quantity < quantityRequested)
            return Result.Failure("Saldo insuficiente");

        Version++;

        if (Sku != null)
            RaiseEvent(new StockReservedEvent(Id, quantityRequested));

        return Result.Success();
    }
    
    public void Release(int quantityToRelease)
    {
        if (quantityToRelease <= 0) return;
        
        RaiseEvent(new StockReleasedEvent(Id, quantityToRelease));
    }
    
    // -------------------------------------------------------------------------------
    public void Apply(StockLotCreatedEvent e)
    {
        Id = e.LotId;
        Sku = new Sku(e.Sku.ToString());
        ZipCode = new ZipCode(e.ZipCode.ToString());
        Quantity = e.Quantity;
        ExpirationDate = e.ExpirationDate;
        IsFragile = e.IsFragile;
    }

    public void Apply(StockReservedEvent e)
    {
        Quantity -= e.Quantity;
    }

    public void Apply(StockReleasedEvent e)
    {
        Quantity += e.Quantity;
    }
}
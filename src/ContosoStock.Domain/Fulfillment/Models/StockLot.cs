using ContosoStock.Domain.Fulfillment.Events;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.BuildingBlocks.Base;
using ContosoStock.Domain.Shared.Helpers;

namespace ContosoStock.Domain.Fulfillment.Models;

public class StockLot : AggregateRoot
{
    public Guid DistributionCenterId { get; private set; }
    public Sku Sku { get; private set; } = null!; // null-forgiving para o EF Core
    public int Quantity { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsFragile { get; private set; }

    // Ctor EF Core e Reidratação
    public StockLot() { }

    public StockLot(
        Guid id, 
        Guid distributionCenterId, 
        Sku sku,
        int quantity, 
        DateTime expirationDate, 
        bool isFragile = false)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero.");

        RaiseEvent(new StockLotCreatedEvent(
            id,
            distributionCenterId,
            sku.Value,
            quantity, 
            expirationDate, 
            isFragile));
    }

    public Result Reserve(int quantityRequested, bool handleFragile = false)
    {
        if (ExpirationDate <= DateTime.UtcNow)
            return Result.Failure("Lote vencido");
        
        if (IsFragile && !handleFragile)
            return Result.Failure("Requer manuseio especial");

        if (Quantity < quantityRequested)
            return Result.Failure("Saldo insuficiente");
        
        RaiseEvent(new StockReservedEvent(Id, quantityRequested));

        return Result.Success();
    }
    
    public void Release(int quantityToRelease)
    {
        if (quantityToRelease <= 0) return;
        
        RaiseEvent(new StockReleasedEvent(Id, quantityToRelease));
    }
    
    // -------------------------------------------------------------------------------
    // EVENT HANDLERS (Mutations) - Devem apenas alterar o estado, sem lógicas de validação
    // -------------------------------------------------------------------------------
    
    public void Apply(StockLotCreatedEvent e)
    {
        Id = e.LotId;
        DistributionCenterId = e.DistributionCenterId; // <-- Faltava isso! xD
        Sku = Sku.Create(e.Sku).Value;
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
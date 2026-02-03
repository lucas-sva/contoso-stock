namespace ContosoStock.Domain.Fulfillment.Models;

public record StockLot(string Id, string Sku, int Quantity, DateTime ExpirationDate, bool IsFragile)
{
    public string Id { get; init; } = Id;
    public string Sku { get; init; } = Sku;
    public int Quantity { get; private set; } = Quantity; // Permitimos alteração interna
    public DateTime ExpirationDate { get; init; } = ExpirationDate;
    public bool IsFragile { get; init; } = IsFragile;

    private bool IsExpired() => ExpirationDate <= DateTime.Now;
    
    public bool Reserve(int quantityRequested, bool handleFragile = false)
    {
        if (IsExpired()) return false;
        if (IsFragile && !handleFragile) return false;
        if (Quantity < quantityRequested) return false;

        Quantity -= quantityRequested;
        return true;
    }
}
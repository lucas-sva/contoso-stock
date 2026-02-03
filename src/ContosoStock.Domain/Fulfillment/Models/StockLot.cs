namespace ContosoStock.Domain.Fulfillment.Models;

public class StockLot(Guid id, Sku sku, ZipCode zipCode, int quantity, DateTime expirationDate, bool isFragile)
{
    public Guid Id { get; } = id;
    public Sku Sku { get; } = sku;
    public ZipCode ZipCode { get; } =  zipCode;
    private int Quantity { get; set; } = quantity;
    private DateTime ExpirationDate { get; } = expirationDate;
    private bool IsFragile { get; } = isFragile;

    public bool Reserve(int quantityRequested, bool handleFragile = false)
    {
        if (ExpirationDate <= DateTime.Now) return false;
        if (IsFragile && !handleFragile) return false;
        if (Quantity < quantityRequested) return false;

        Quantity -= quantityRequested;
        return true;
    }
}
namespace ContosoStock.Domain.Fulfillment.ValueObjects;

public record Sku
{
    private string Value { get; } = null!;

    public Sku(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            throw new ArgumentException("Sku inválido");
        
        Value = value.ToUpper();
    }
    
    public override string ToString() => Value;
}
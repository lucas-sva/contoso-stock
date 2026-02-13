using ContosoStock.Domain.Shared.Helpers;

namespace ContosoStock.Domain.Fulfillment.ValueObjects;

public record Sku
{
    public string Value { get; }

    private Sku(string value)
    {
        Value = value;
    }
    
    public static Result<Sku> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Sku>("O SKU não pode ser vazio.");
        
        return value.Length < 5 ? Result.Failure<Sku>("SKU inválido. Deve ter pelo menos 5 caracteres.") :
            Result.Success(new Sku(value.ToUpper()));
    }

    public override string ToString() => Value;
}
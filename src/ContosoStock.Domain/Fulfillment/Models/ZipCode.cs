using System.Text.RegularExpressions;

namespace ContosoStock.Domain.Fulfillment.Models;

public partial record ZipCode
{
    private string Value { get; } = null!;

    public ZipCode(string value)
    {
        if(!IsValid(value))
            throw new ArgumentException($"Cep com formato inválido: {value}");
        
        Value = value;
    }

    private static bool IsValid(string value)
    {
        return !string.IsNullOrEmpty(value) && MyRegex().IsMatch(value);
    }
    public static string GetRegionCode(string value)
    {
        if(!IsValid(value))
            throw new ArgumentException($"Cep com formato inválido: {value}");
        
        var digits = value.Replace("-", "");
        
        var prefix = int.Parse(digits[..2]);

        foreach (var kvp in from kvp in RegionCodes 
                 let ranges = kvp.Value.Split('-') 
                 let start = int.Parse(ranges[0]) 
                 let end = ranges.Length > 1 ? int.Parse(ranges[1]) : start 
                 where prefix >= start && prefix <= end 
                 select kvp)
        {
            return kvp.Key;
        }

        return "Região desconhecida";
    }
    
    private static readonly Dictionary<string, string> RegionCodes = new()
    {
        { "SP", "01-19" }, // São Paulo
        { "RJ", "20-28" }, // Rio de Janeiro
        { "ES", "29" },    // Espírito Santo
        { "MG", "30-39" }, // Minas Gerais
        { "BA", "40-48" }, // Bahia
        { "SE", "49" },    // Sergipe
        { "PE", "50-56" }, // Pernambuco
        { "AL", "57" },    // Alagoas
        { "PB", "58" },    // Paraíba
        { "RN", "59" },    // Rio Grande do Norte
        { "CE", "60-63" }, // Ceará
        { "PI", "64" },    // Piauí
        { "MA", "65-65" }, // Maranhão
        { "PA", "66-68" }, // Pará
        { "AM", "69" },    // Amazonas
        { "AC", "69" },    // Acre (compartilha faixa)
        { "RO", "69" },    // Rondônia
        { "RR", "69" },    // Roraima
        { "TO", "77" },    // Tocantins
        { "DF", "70-73" }, // Distrito Federal
        { "GO", "74-76" }, // Goiás
        { "MS", "79" },    // Mato Grosso do Sul
        { "MT", "78" },    // Mato Grosso
        { "PR", "80-87" }, // Paraná
        { "SC", "88-89" }, // Santa Catarina
        { "RS", "90-99" }  // Rio Grande do Sul
    };
    
    [GeneratedRegex(@"^\d{5}-\d{3}$")]
    private static partial Regex MyRegex();
}
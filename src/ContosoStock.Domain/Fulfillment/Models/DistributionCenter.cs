using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.BuildingBlocks.Base;

namespace ContosoStock.Domain.Fulfillment.Models;

public class DistributionCenter : AggregateRoot
{
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    
    private readonly List<string> _supportedRegions = [];
    public IReadOnlyCollection<string> SupportedRegions => _supportedRegions.AsReadOnly();
    
    // ROA
    public bool CanFulfill(ZipCode destination)
    {
        var region = ZipCode.GetRegionCode(destination.Value); 
        return _supportedRegions.Contains(region);
    }
    
    public DistributionCenter() { }
}
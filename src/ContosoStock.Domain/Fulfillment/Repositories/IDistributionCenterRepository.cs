using ContosoStock.Domain.Fulfillment.Models;

namespace ContosoStock.Domain.Fulfillment.Repositories;

public interface IDistributionCenterRepository
{
    Task<IEnumerable<DistributionCenter>> GetAllActiveAsync(CancellationToken cancellationToken);
}
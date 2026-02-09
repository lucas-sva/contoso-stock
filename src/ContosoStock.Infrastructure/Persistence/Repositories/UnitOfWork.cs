using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;
using ContosoStock.Infrastructure.Persistence.Contexts;

namespace ContosoStock.Infrastructure.Persistence.Repositories;

public class UnitOfWork(ContosoStockDbContext dbContext) : IUnitOfWork
{
    private readonly ContosoStockDbContext _dbContext = dbContext;

    public async Task CommitAsync(CancellationToken cancellationToken = default) 
        => await _dbContext.SaveChangesAsync(cancellationToken);
    
}
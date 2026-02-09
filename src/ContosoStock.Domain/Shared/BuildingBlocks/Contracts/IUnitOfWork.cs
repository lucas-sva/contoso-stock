namespace ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
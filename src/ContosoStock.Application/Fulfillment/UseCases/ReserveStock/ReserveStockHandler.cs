using ContosoStock.Domain.Fulfillment.Repositories;
using ContosoStock.Domain.Fulfillment.Services;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.Helpers;
using MediatR;

namespace ContosoStock.Application.Fulfillment.UseCases.ReserveStock;

public class ReserveStockHandler(
    IStockLotRepository lotRepository,
    IDistributionCenterRepository cdRepository,
    AllocationService allocationService)
    : IRequestHandler<ReserveStockCommand, Result>
{
    private readonly IStockLotRepository _lotRepository = lotRepository;
    private readonly IDistributionCenterRepository _cdRepository = cdRepository;
    private readonly AllocationService _allocationService =  allocationService;

    public async Task<Result> Handle(ReserveStockCommand request, CancellationToken cancellationToken)
    {
        // Valida os VOs
        var skuResult = Sku.Create(request.Sku);
        if (skuResult.IsFailure) 
            return Result.Failure(skuResult.Error);

        var zipResult = ZipCode.Create(request.DestinationZipCode);
        if (zipResult.IsFailure) 
            return Result.Failure(zipResult.Error);

        // Busca os models
        var activeCds = await _cdRepository.GetAllActiveAsync(cancellationToken);
        var availableLots = await _lotRepository.GetBySkuAsync(skuResult.Value, cancellationToken);
        
        var targetLot = _allocationService.ExecuteRao(
            zipResult.Value, 
            activeCds, 
            availableLots, 
            request.Quantity);

        if (targetLot is null)
            return Result.Failure($"Nenhum lote disponível no estoque para atender a região {ZipCode.GetRegionCode(request.DestinationZipCode)} com a quantidade solicitada.");
        
        var reserveResult = targetLot.Reserve(request.Quantity, request.HandleFragile);

        if (reserveResult.IsFailure)
            return reserveResult;
        
        await _lotRepository.UpdateAsync(targetLot, cancellationToken);
        
        return Result.Success();
    }
}
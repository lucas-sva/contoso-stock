using ContosoStock.Domain.Fulfillment.Models;
using ContosoStock.Domain.Fulfillment.Ports.Contracts;
using ContosoStock.Domain.Fulfillment.Services;
using ContosoStock.Domain.Fulfillment.ValueObjects;
using ContosoStock.Domain.Shared.BuildingBlocks.Contracts;

namespace ContosoStock.Application.Fulfillment.UseCases.ReserveStock;

public class ReserveStockHandler(
    IStockLotRepository repository,
    AllocationService allocationService,
    IUnitOfWork unitOfWork)
{
    private readonly IStockLotRepository _repository = repository;
    private readonly AllocationService _allocationService = allocationService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ReserveStockResult> Handle(ReserveStockCommand request, CancellationToken cancellationToken)
    {
        // DTO -> Value Object
        Sku sku;
        ZipCode zipCode;

        try
        {
            sku = new Sku(request.Sku);
            zipCode = new ZipCode(request.ZipCode);
        }
        catch (ArgumentException e)
        {
            return new ReserveStockResult(false, $"Dados inválidos: {e.Message}");
        }
        
        // Orquestração: FEFO in memory
        var lots = await _repository.GetBySkuAsync(sku);
        var targetLot = lots
            .Where(lot => lot.Quantity >= request.Quantity)  // Possui quantidade disponível
            .Where(lot => lot.ExpirationDate > DateTime.Now) // Não está expirado
            .OrderBy(lot => lot.ExpirationDate)              // FEFO
            .FirstOrDefault();

        if (targetLot == null)
            return new ReserveStockResult(false, "Nenhum lote disponível com saldo suficiente.");
        
        
        // Lógica de negócio do Domain
        try
        {
            _allocationService.ProcessOrderFulfillment(
                request.SaleId,
                targetLot,
                new List<DistributionCenter>(),
                zipCode,
                request.Quantity
                );
        }
        catch (Exception e)
        {
            return new ReserveStockResult(false, e.Message);
        }
        
        // Persistência
        await _repository.UpdateAsync(targetLot);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ReserveStockResult(true, "Reserva realizada com sucesso.", targetLot.Id);
    }
}
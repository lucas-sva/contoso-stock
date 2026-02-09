namespace ContosoStock.Application.Fulfillment.UseCases.ReserveStock;

public record ReserveStockResult(
    bool Success,
    string Message,
    Guid? ReservedLotId = null
);
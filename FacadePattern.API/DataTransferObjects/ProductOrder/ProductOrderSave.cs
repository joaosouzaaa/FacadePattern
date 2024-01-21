namespace FacadePattern.API.DataTransferObjects.ProductOrder;

public sealed record ProductOrderSave(int Quantity,
                                     int ProductId);

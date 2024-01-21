namespace FacadePattern.API.DataTransferObjects.ProductOrder;

public sealed class ProductOrderSave
{
    public required int Quantity { get; set; }
    public required int ProductId { get; set; }
}

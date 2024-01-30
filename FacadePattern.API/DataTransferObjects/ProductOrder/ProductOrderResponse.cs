namespace FacadePattern.API.DataTransferObjects.ProductOrder;

public sealed class ProductOrderResponse
{
    public required int Id { get; set; }
    public required int Quantity { get; set; }
    public required decimal TotalValue { get; set; }
}

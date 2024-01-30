using FacadePattern.API.DataTransferObjects.ProductOrder;

namespace FacadePattern.API.DataTransferObjects.Order;

public sealed class OrderResponse
{
    public required int Id { get; set; }
    public required decimal TotalValue { get; set; }
    public required DateTime CreationDate { get; set; }
    public required List<ProductOrderResponse> ProductsOrder { get; set; }
}

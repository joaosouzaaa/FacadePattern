using FacadePattern.API.DataTransferObjects.ProductOrder;

namespace FacadePattern.API.DataTransferObjects.Order;

public sealed class OrderSave
{
    public string? CouponName { get; set; }
    public List<ProductOrderSave> ProductsOrder{ get; set; }
}

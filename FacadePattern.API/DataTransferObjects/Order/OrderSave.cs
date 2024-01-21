using FacadePattern.API.DataTransferObjects.ProductOrder;

namespace FacadePattern.API.DataTransferObjects.Order;

public sealed record OrderSave(string? CouponName,
                               List<ProductOrderSave> ProductsOrder);

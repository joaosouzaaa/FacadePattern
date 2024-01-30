using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Interfaces.Services;

namespace FacadePattern.API.Services;

public sealed class EcommerceFacade(ICouponService couponService, IProductService productService)
{
    private readonly ICouponService _couponService = couponService;
    private readonly IProductService _productService = productService;

    public async Task<bool> PlaceOrderAsync(OrderSave orderSave)
    {
        if(orderSave.CouponName is not null && !await _couponService.IsCouponValid(orderSave.CouponName))
            return false;

        if(!await _productService.IsProductListValidAsync(orderSave.ProductsOrder))
            return false;

        

        // decrease inventory

        // call supplier

        return true;
    }
}

using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Entities;
using FacadePattern.API.Enums;
using FacadePattern.API.Extensions;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Interfaces.Settings;

namespace FacadePattern.API.Services;

public sealed class EcommerceFacade(ICouponService couponService, IProductOrderService productOrderService, 
                                    IOrderService orderService, IInventoryService inventoryService,
                                    INotificationHandler notificationHandler) : IEcommerceFacade
{
    public async Task<bool> PlaceOrderAsync(OrderSave orderSave)
    {
        if (!IsOrderValid(orderSave))
        {
            notificationHandler.AddNotification(nameof(EMessage.Invalid), EMessage.Invalid.Description().FormatTo("Order"));

            return false;
        }

        var order = new Order()
        {
            CreationDate = DateTime.UtcNow,
            ProductsOrder = []
        };

        if (!await productOrderService.ProcessProductOrderListAsync(orderSave.ProductsOrder, order.ProductsOrder))
            return false;

        order.TotalValue = order.ProductsOrder.Select(p => p.TotalValue).Sum();

        if (orderSave.CouponName is not null && !await couponService.ProcessDiscountAsync(order, orderSave.CouponName))
            return false;

        await orderService.AddAsync(order);

        await inventoryService.DecreaseInventoryQuantityByOrderAsync(order);

        // call supplier

        return true;
    }

    private static bool IsOrderValid(OrderSave orderSave) =>
        !orderSave.ProductsOrder.Any() 
        && orderSave.ProductsOrder.Select(p => p.ProductId).Distinct().Count() != orderSave.ProductsOrder.Count;
}

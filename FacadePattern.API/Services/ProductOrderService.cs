using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;
using FacadePattern.API.Enums;
using FacadePattern.API.Extensions;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Interfaces.Settings;

namespace FacadePattern.API.Services;

public sealed class ProductOrderService(IProductService productService, INotificationHandler notificationHandler) : IProductOrderService
{
    public async Task<bool> ProcessProductOrderListAsync(List<ProductOrderSave> productOrderSaveList, List<ProductOrder> productOrderList)
    {
        foreach (var productOrderSave in productOrderSaveList)
        {
            var product = await productService.GetByIdRetunsDomainObjectAsync(productOrderSave.ProductId);

            if (product is null)
            {
                notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Product"));

                return false;
            }

            var productOrderQuantity = productOrderSave.Quantity;
            if (productOrderQuantity > product.Inventory.Quantity)
            {
                notificationHandler.AddNotification(nameof(EMessage.Invalid), EMessage.Invalid.Description().FormatTo("Product"));

                return false;
            }

            productOrderList.Add(new ProductOrder()
            {
                ProductId = product.Id,
                Quantity = productOrderQuantity,
                TotalValue = productOrderQuantity * product.Price
            });
        }

        return true;
    }
}

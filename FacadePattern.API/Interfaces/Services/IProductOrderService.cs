using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Services;

public interface IProductOrderService
{
    Task<bool> ProcessProductOrderListAsync(List<ProductOrderSave> productOrderSaveList, List<ProductOrder> productOrderList);
}

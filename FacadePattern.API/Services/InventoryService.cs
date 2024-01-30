using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Repositories;
using FacadePattern.API.Interfaces.Services;

namespace FacadePattern.API.Services;

public sealed class InventoryService(IInventoryRepository inventoryRepository) : IInventoryService
{
    public async Task DecreaseInventoryQuantityByOrderAsync(Order order)
    {
        foreach(var productOrder in order.ProductsOrder)
        {
            var productId = productOrder.ProductId;

            var quantity = await inventoryRepository.GetQuantityByProductIdAsync(productId);

            await inventoryRepository.UpdateQuantityByProductIdAsync(productId, quantity - productOrder.Quantity);
        }
    }
}

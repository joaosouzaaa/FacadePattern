using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Services;

public interface IInventoryService
{
    Task DecreaseInventoryQuantityByOrderAsync(Order order);
}

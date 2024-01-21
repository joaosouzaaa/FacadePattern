using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Repositories;

public interface IInventoryRepository
{
    Task<bool> UpdateQuantityAsync(Inventory inventory);
}

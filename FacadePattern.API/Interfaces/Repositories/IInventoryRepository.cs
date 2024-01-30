using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Repositories;

public interface IInventoryRepository
{
    Task<int> GetQuantityByProductIdAsync(int productId);
    Task UpdateQuantityByProductIdAsync(int productId, int quantity);
}

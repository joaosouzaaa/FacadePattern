using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Repositories;

public interface IProductRepository
{
    Task<bool> AddAsync(Product product);
    Task<bool> ExistsAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<List<Product>> GetAllAsync();
}

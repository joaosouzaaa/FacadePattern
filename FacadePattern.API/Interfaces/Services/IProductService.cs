using FacadePattern.API.DataTransferObjects.Product;

namespace FacadePattern.API.Interfaces.Services;

public interface IProductService
{
    Task<bool> AddAsync(ProductSave productSave);
    Task<bool> DeleteAsync(int id);
    Task<List<ProductResponse>> GetAllAsync();
}

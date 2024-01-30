using FacadePattern.API.DataTransferObjects.Product;
using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Services;

public interface IProductService
{
    Task<bool> AddAsync(ProductSave productSave);
    Task<bool> DeleteAsync(int id);
    Task<List<ProductResponse>> GetAllAsync();
    Task<Product?> GetByIdRetunsDomainObjectAsync(int id);
}

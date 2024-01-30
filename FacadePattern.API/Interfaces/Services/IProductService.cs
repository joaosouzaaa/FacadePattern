using FacadePattern.API.DataTransferObjects.Product;
using FacadePattern.API.DataTransferObjects.ProductOrder;

namespace FacadePattern.API.Interfaces.Services;

public interface IProductService
{
    Task<bool> AddAsync(ProductSave productSave);
    Task<bool> DeleteAsync(int id);
    Task<List<ProductResponse>> GetAllAsync();
    Task<bool> IsProductListValidAsync(List<ProductOrderSave> productOrderSaveList);
}

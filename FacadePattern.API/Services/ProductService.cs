using FacadePattern.API.DataTransferObjects.Product;
using FacadePattern.API.Entities;
using FacadePattern.API.Enums;
using FacadePattern.API.Extensions;
using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Interfaces.Repositories;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Interfaces.Settings;

namespace FacadePattern.API.Services;

public sealed class ProductService(IProductRepository productRepository, IProductMapper productMapper, 
                                   INotificationHandler notificationHandler) : IProductService
{
    public async Task<bool> AddAsync(ProductSave productSave)
    {
        if (!IsValid(productSave))
        {
            notificationHandler.AddNotification(nameof(EMessage.Invalid), EMessage.Invalid.Description().FormatTo("Product"));

            return false;
        }

        var product = productMapper.SaveToDomain(productSave);

        return await productRepository.AddAsync(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await productRepository.ExistsAsync(id))
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Product"));

            return false;
        }

        return await productRepository.DeleteAsync(id);
    }

    public async Task<List<ProductResponse>> GetAllAsync()
    {
        var productList = await productRepository.GetAllAsync();

        return productMapper.DomainListToResponseList(productList);
    }

    public Task<Product?> GetByIdRetunsDomainObjectAsync(int id) =>
        productRepository.GetByIdAsync(id);

    private static bool IsValid(ProductSave productSave) =>
        !string.IsNullOrEmpty(productSave.Name)
        && productSave.Name.Length > 2
        && productSave.Name.Length < 100
        && productSave.Price > 0
        && productSave.QuantityAvailable > 0;
}

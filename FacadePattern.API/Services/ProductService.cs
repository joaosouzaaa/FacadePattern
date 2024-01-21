using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.DataTransferObjects.Product;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Interfaces.Repositories;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Interfaces.Settings;

namespace FacadePattern.API.Services;

public sealed class ProductService(IProductRepository productRepository, IProductMapper productMapper, 
                                   INotificationHandler notificationHandler) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductMapper _productMapper = productMapper;
    private readonly INotificationHandler _notificationHandler = notificationHandler;

    public async Task<bool> AddAsync(ProductSave productSave)
    {
        if (!IsValid(productSave))
        {
            _notificationHandler.AddNotification("Invalid product", "The product is invalid");

            return false;
        }

        var product = _productMapper.SaveToDomain(productSave);

        return await _productRepository.AddAsync(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _productRepository.ExistsAsync(id))
        {
            _notificationHandler.AddNotification("Not found", "Product was not found.");

            return false;
        }

        return await _productRepository.DeleteAsync(id);
    }

    public async Task<List<ProductResponse>> GetAllAsync()
    {
        var productList = await _productRepository.GetAllAsync();

        return _productMapper.DomainListToResponseList(productList);
    }

    private static bool IsValid(ProductSave productSave) =>
        !string.IsNullOrEmpty(productSave.Name)
        && productSave.Name.Length > 2
        && productSave.Price > 0
        && productSave.QuantityAvailable > 0;
}

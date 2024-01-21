using FacadePattern.API.DataTransferObjects.Product;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;

namespace FacadePattern.API.Mappers;

public sealed class ProductMapper(IInventoryMapper inventoryMapper) : IProductMapper
{
    private readonly IInventoryMapper _inventoryMapper = inventoryMapper;

    public Product SaveToDomain(ProductSave productSave) =>
        new()
        {
            Name = productSave.Name,
            Price = productSave.Price,
            Inventory = new()
            {
                Quantity = productSave.QuantityAvailable
            }
        };

    public List<ProductResponse> DomainListToResponseList(List<Product> productList) =>
        productList.Select(DomainToResponse).ToList();

    private ProductResponse DomainToResponse(Product product) =>
        new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Inventory = _inventoryMapper.DomainToResponse(product.Inventory)
        };
}

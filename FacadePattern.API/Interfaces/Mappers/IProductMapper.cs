using FacadePattern.API.DataTransferObjects.Product;
using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Mappers;

public interface IProductMapper
{
    Product SaveToDomain(ProductSave productSave);
    List<ProductResponse> DomainListToResponseList(List<Product> productList);
}

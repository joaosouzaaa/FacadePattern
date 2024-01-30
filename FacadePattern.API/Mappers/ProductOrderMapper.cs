using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;

namespace FacadePattern.API.Mappers;

public sealed class ProductOrderMapper : IProductOrderMapper
{
    public List<ProductOrderResponse> DomainListToResponseList(List<ProductOrder> productOrderList) =>
        productOrderList.Select(DomainToResponse).ToList();

    private ProductOrderResponse DomainToResponse(ProductOrder productOrder) =>
        new()
        {
            Id = productOrder.Id,
            Quantity = productOrder.Quantity,
            TotalValue = productOrder.TotalValue
        };
}

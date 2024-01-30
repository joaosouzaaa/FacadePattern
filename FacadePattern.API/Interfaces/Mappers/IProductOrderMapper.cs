using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Mappers;

public interface IProductOrderMapper
{
    List<ProductOrderResponse> DomainListToResponseList(List<ProductOrder> productOrderList);
}

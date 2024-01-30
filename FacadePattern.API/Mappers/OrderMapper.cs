using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;

namespace FacadePattern.API.Mappers;

public sealed class OrderMapper(IProductOrderMapper productOrderMapper) : IOrderMapper
{
    public List<OrderResponse> DomainListToResponseList(List<Order> orderList) =>
        orderList.Select(DomainToResponse).ToList();

    private OrderResponse DomainToResponse(Order order) =>
        new()
        {
            CreationDate = order.CreationDate,
            Id = order.Id,
            ProductsOrder = productOrderMapper.DomainListToResponseList(order.ProductsOrder),
            TotalValue = order.TotalValue
        };
}

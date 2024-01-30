using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Mappers;

public interface IOrderMapper
{
    List<OrderResponse> DomainListToResponseList(List<Order> orderList);
}

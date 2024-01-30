using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Interfaces.Repositories;
using FacadePattern.API.Interfaces.Services;

namespace FacadePattern.API.Services;

public sealed class OrderService(IOrderRepository orderRepository, IOrderMapper orderMapper) : IOrderService
{
    public Task AddAsync(Order order) =>
        orderRepository.AddAsync(order);

    public async Task<List<OrderResponse>> GetAllAsync()
    {
        var orderList = await orderRepository.GetAllAsync();

        return orderMapper.DomainListToResponseList(orderList);
    }
}

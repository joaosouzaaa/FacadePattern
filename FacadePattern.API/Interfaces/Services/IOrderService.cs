using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Services;

public interface IOrderService
{
    Task AddAsync(Order order);
    Task<List<OrderResponse>> GetAllAsync();
}

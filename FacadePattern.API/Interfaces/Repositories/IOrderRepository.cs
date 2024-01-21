using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<bool> AddAsync(Order order);
    Task<List<Order>> GetAllAsync();
}

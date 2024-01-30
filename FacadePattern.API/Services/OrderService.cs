using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Interfaces.Services;

namespace FacadePattern.API.Services;

public sealed class OrderService : IOrderService
{
    public async Task<bool> AddAsync(OrderSave orderSave)
    {

    }
}

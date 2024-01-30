using FacadePattern.API.DataTransferObjects.Order;

namespace FacadePattern.API.Interfaces.Services;

public interface IEcommerceFacade
{
    Task<bool> PlaceOrderAsync(OrderSave orderSave);
}

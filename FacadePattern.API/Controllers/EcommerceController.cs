using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Settings.NotificationHandlerSettings;
using Microsoft.AspNetCore.Mvc;

namespace FacadePattern.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class EcommerceController(IEcommerceFacade ecommerceFacade) : ControllerBase
{
    [HttpPost("place-order")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> PlaceOrderAsync([FromBody] OrderSave orderSave) =>
        ecommerceFacade.PlaceOrderAsync(orderSave);
}
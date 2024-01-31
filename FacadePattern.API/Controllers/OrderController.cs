using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FacadePattern.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<OrderResponse>> GetAllAsync() =>
        orderService.GetAllAsync();
}

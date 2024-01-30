using FacadePattern.API.DataTransferObjects.Product;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Settings.NotificationHandlerSettings;
using Microsoft.AspNetCore.Mvc;

namespace FacadePattern.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class ProductController(IProductService productService) : ControllerBase
{
    [HttpPost("add-product")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] ProductSave productSave) =>
        productService.AddAsync(productSave);

    [HttpDelete("delete-product")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> DeleteAsync([FromQuery] int id) =>
        productService.DeleteAsync(id);

    [HttpGet("get-all-products")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<ProductResponse>> GetAllAsync() =>
        productService.GetAllAsync();
}

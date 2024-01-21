using FacadePattern.API.DataTransferObjects.Inventory;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;

namespace FacadePattern.API.Mappers;

public sealed class InventoryMapper : IInventoryMapper
{
    public InventoryResponse DomainToResponse(Inventory inventory) =>
        new()
        {
            Id = inventory.Id,
            Quantity = inventory.Quantity
        };
}

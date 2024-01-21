using FacadePattern.API.DataTransferObjects.Inventory;
using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Mappers;

public interface IInventoryMapper
{
    InventoryResponse DomainToResponse(Inventory inventory);
}

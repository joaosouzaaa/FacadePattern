using FacadePattern.API.DataTransferObjects.Inventory;

namespace FacadePattern.API.DataTransferObjects.Product;

public sealed class ProductResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }

    public required InventoryResponse Inventory { get; set; }
}

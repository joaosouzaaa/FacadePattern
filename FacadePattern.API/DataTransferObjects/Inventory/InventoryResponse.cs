namespace FacadePattern.API.DataTransferObjects.Inventory;

public sealed class InventoryResponse
{
    public required int Id { get; set; }
    public required int Quantity { get; set; }
}

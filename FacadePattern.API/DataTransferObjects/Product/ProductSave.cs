namespace FacadePattern.API.DataTransferObjects.Product;

public sealed class ProductSave
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int QuantityAvailable { get; set; }
}

namespace FacadePattern.API.Entities;

public sealed class Inventory
{
    public int Id { get; set; }
    public required int Quantity { get; set; }
}

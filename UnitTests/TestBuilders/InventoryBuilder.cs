using FacadePattern.API.DataTransferObjects.Inventory;
using FacadePattern.API.Entities;

namespace UnitTests.TestBuilders;
public sealed class InventoryBuilder
{
    private readonly int _id = 123;
    private readonly int _quantity = 123;

    public static InventoryBuilder NewObject() =>
        new();

    public Inventory DomainBuild() =>
        new()
        {
            Id = _id,
            Quantity = _quantity,
            ProductId = 1
        };

    public InventoryResponse ResponseBuild() =>
        new()
        {
            Id = _id,
            Quantity = _quantity
        };
}

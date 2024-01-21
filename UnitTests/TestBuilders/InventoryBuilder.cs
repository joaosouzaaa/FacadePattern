using FacadePattern.API.DataTransferObjects.Inventory;
using FacadePattern.API.Entities;

namespace UnitTests.TestBuilders;
public sealed class InventoryBuilder
{
    private int _quantity = 123;

    public static InventoryBuilder NewObject() =>
        new();

    public Inventory DomainBuild() =>
        new()
        {
            Id = 1,
            Quantity = _quantity,
            ProductId = 1
        };

    public InventoryResponse ResponseBuild() =>
        new()
        {
            Id = 1,
            Quantity = _quantity
        };
}

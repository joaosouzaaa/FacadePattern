using FacadePattern.API.DataTransferObjects.Product;
using FacadePattern.API.Entities;

namespace UnitTests.TestBuilders;
public sealed class ProductBuilder
{
    private string _name = "test";
    private decimal _price = 22.98m;
    private int _quantityAvailable = 1;

    public static ProductBuilder NewObject() =>
        new();

    public Product DomainBuild() =>
        new()
        {
            Id = 1,
            Inventory = InventoryBuilder.NewObject().DomainBuild(),
            Name = _name,
            Price = _price
        };

    public ProductSave SaveBuild() =>
        new(_name,
            _price,
            _quantityAvailable);

    public ProductResponse ResponseBuild() =>
        new()
        {
            Id = 8,
            Inventory = InventoryBuilder.NewObject().ResponseBuild(),
            Name = _name,
            Price = _price
        };

    public ProductBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public ProductBuilder WithPrice(decimal price)
    {
        _price = price;

        return this;
    }

    public ProductBuilder WithQuantityAvailable(int quantityAvailable)
    {
        _quantityAvailable = quantityAvailable;

        return this;
    }
}

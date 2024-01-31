using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;

namespace UnitTests.TestBuilders;
public sealed class ProductOrderBuilder
{
    private readonly int _id = 13;
    private int _productId = 123;
    private int _quantity = 123;
    private decimal _totalValue = 98;

    public static ProductOrderBuilder NewObject() =>
        new();

    public ProductOrder DomainBuild() =>
        new()
        {
            Id = _id,
            ProductId = _productId,
            Quantity = _quantity,
            TotalValue = _totalValue
        };

    public ProductOrderResponse ResponseBuild() =>
        new()
        {
            Id = _id,
            Quantity = _quantity,
            TotalValue = _totalValue
        };

    public ProductOrderSave SaveBuild() =>
        new(_quantity,
            _productId);

    public ProductOrderBuilder WithTotalValue(decimal totalValue)
    {
        _totalValue = totalValue;

        return this;
    }

    public ProductOrderBuilder WithProductId(int productId)
    {
        _productId = productId;

        return this;
    }

    public ProductOrderBuilder WithQuantity(int quantity)
    {
        _quantity = quantity;

        return this;
    }
}

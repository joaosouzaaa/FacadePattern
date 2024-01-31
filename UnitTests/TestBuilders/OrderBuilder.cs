using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;

namespace UnitTests.TestBuilders;
public sealed class OrderBuilder
{
    private readonly int _id = 123;
    private readonly DateTime _creationDate = DateTime.Now;
    private decimal _totalValue = 28.98m;
    private List<ProductOrder> _productOrderList = [];
    private string? _couponName;
    private List<ProductOrderSave> _productOrderSaveList = [];

    public static OrderBuilder NewObject() =>
        new();

    public Order DomainBuild() =>
        new()
        {
            CreationDate = _creationDate,
            Id = _id,
            ProductsOrder = _productOrderList,
            TotalValue = _totalValue
        };

    public OrderResponse ResponseBuild() =>
        new()
        {
            CreationDate = _creationDate,
            Id = _id,
            ProductsOrder = [],
            TotalValue = _totalValue
        };

    public OrderSave SaveBuild() =>
        new(_couponName,
            _productOrderSaveList);

    public OrderBuilder WithProductOrderList(List<ProductOrder> productOrderList)
    {
        _productOrderList = productOrderList;

        return this;
    }

    public OrderBuilder WithTotalValue(decimal totalValue)
    {
        _totalValue = totalValue;

        return this;
    }

    public OrderBuilder WithCouponName(string? couponName)
    {
        _couponName = couponName;

        return this;
    }

    public OrderBuilder WithProductOrderSaveList(List<ProductOrderSave> productOrderSaveList)
    {
        _productOrderSaveList = productOrderSaveList;

        return this;
    }
}

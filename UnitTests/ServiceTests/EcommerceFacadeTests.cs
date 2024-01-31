using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Interfaces.Settings;
using FacadePattern.API.Services;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServiceTests;
public sealed class EcommerceFacadeTests
{
    private readonly Mock<ICouponService > _couponServiceMock;
    private readonly Mock<IProductOrderService> _productOrderServiceMock;
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly Mock<IInventoryService> _inventoryServiceMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly EcommerceFacade _ecommerceFacade;

    public EcommerceFacadeTests()
    {
        _couponServiceMock = new Mock<ICouponService>();
        _productOrderServiceMock = new Mock<IProductOrderService>();
        _orderServiceMock = new Mock<IOrderService>();
        _inventoryServiceMock = new Mock<IInventoryService>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _ecommerceFacade = new EcommerceFacade(_couponServiceMock.Object, _productOrderServiceMock.Object, _orderServiceMock.Object,
            _inventoryServiceMock.Object, _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task PlaceOrderAsync_CouponIsNull_SuccessfulScenario()
    {
        // A
        var productOrderSaveList = new List<ProductOrderSave>()
        {
            ProductOrderBuilder.NewObject().WithProductId(123).SaveBuild(),
            ProductOrderBuilder.NewObject().WithProductId(9).SaveBuild()
        };
        var orderSave = OrderBuilder.NewObject().WithProductOrderSaveList(productOrderSaveList).WithCouponName(null).SaveBuild();

        _productOrderServiceMock.Setup(p => p.ProcessProductOrderListAsync(It.IsAny<List<ProductOrderSave>>(), It.IsAny<List<ProductOrder>>()))
            .ReturnsAsync(true);

        // A
        await _ecommerceFacade.PlaceOrderAsync(orderSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _couponServiceMock.Verify(c => c.ProcessDiscountAsync(It.IsAny<Order>(), It.IsAny<string>()), Times.Never());
        _orderServiceMock.Verify(o => o.AddAsync(It.IsAny<Order>()), Times.Once());
        _inventoryServiceMock.Verify(i => i.DecreaseInventoryQuantityByOrderAsync(It.IsAny<Order>()), Times.Once());
    }

    [Fact]
    public async Task PlaceOrderAsync_CouponIsNotNull_SuccessfulScenario()
    {
        // A
        var productOrderSaveList = new List<ProductOrderSave>()
        {
            ProductOrderBuilder.NewObject().WithProductId(123).SaveBuild(),
            ProductOrderBuilder.NewObject().WithProductId(9).SaveBuild()
        };
        var orderSave = OrderBuilder.NewObject().WithProductOrderSaveList(productOrderSaveList).WithCouponName("test").SaveBuild();

        _productOrderServiceMock.Setup(p => p.ProcessProductOrderListAsync(It.IsAny<List<ProductOrderSave>>(), It.IsAny<List<ProductOrder>>()))
            .ReturnsAsync(true);

        _couponServiceMock.Setup(c => c.ProcessDiscountAsync(It.IsAny<Order>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        // A
        await _ecommerceFacade.PlaceOrderAsync(orderSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _couponServiceMock.Verify(c => c.ProcessDiscountAsync(It.IsAny<Order>(), It.IsAny<string>()), Times.Once());
        _orderServiceMock.Verify(o => o.AddAsync(It.IsAny<Order>()), Times.Once());
        _inventoryServiceMock.Verify(i => i.DecreaseInventoryQuantityByOrderAsync(It.IsAny<Order>()), Times.Once());
    }

    [Theory]
    [MemberData(nameof(PlaceOrderInvalidParameters))]
    public async Task PlaceOrderAsync_OrderIsInvalid_InvalidScenario(OrderSave orderSave)
    {
        await _ecommerceFacade.PlaceOrderAsync(orderSave);

        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _productOrderServiceMock.Verify(p => p.ProcessProductOrderListAsync(It.IsAny<List<ProductOrderSave>>(), It.IsAny<List<ProductOrder>>()), Times.Never());
        _couponServiceMock.Verify(c => c.ProcessDiscountAsync(It.IsAny<Order>(), It.IsAny<string>()), Times.Never());
        _orderServiceMock.Verify(o => o.AddAsync(It.IsAny<Order>()), Times.Never());
        _inventoryServiceMock.Verify(i => i.DecreaseInventoryQuantityByOrderAsync(It.IsAny<Order>()), Times.Never());
    }

    public static IEnumerable<object[]> PlaceOrderInvalidParameters()
    {
        yield return new object[]
        {
            OrderBuilder.NewObject().WithProductOrderSaveList([]).SaveBuild()
        };

        var productOrderSaveList = new List<ProductOrderSave>()
        {
            ProductOrderBuilder.NewObject().WithProductId(123).SaveBuild(),
            ProductOrderBuilder.NewObject().WithProductId(123).SaveBuild()
        };
        var orderSaveWithInvalidProductOrderList = OrderBuilder.NewObject().WithProductOrderSaveList(productOrderSaveList).SaveBuild();

        yield return new object[]
        {
            orderSaveWithInvalidProductOrderList
        };
    }

    [Fact]
    public async Task PlaceOrderAsync_ProcessProductOrderListReturnsInvalid_InvalidScenario()
    {
        // A
        var productOrderSaveList = new List<ProductOrderSave>()
        {
            ProductOrderBuilder.NewObject().WithProductId(123).SaveBuild()
        };
        var orderSave = OrderBuilder.NewObject().WithProductOrderSaveList(productOrderSaveList).SaveBuild();

        _productOrderServiceMock.Setup(p => p.ProcessProductOrderListAsync(It.IsAny<List<ProductOrderSave>>(), It.IsAny<List<ProductOrder>>()))
            .ReturnsAsync(false);

        // A
        await _ecommerceFacade.PlaceOrderAsync(orderSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _couponServiceMock.Verify(c => c.ProcessDiscountAsync(It.IsAny<Order>(), It.IsAny<string>()), Times.Never());
        _orderServiceMock.Verify(o => o.AddAsync(It.IsAny<Order>()), Times.Never());
        _inventoryServiceMock.Verify(i => i.DecreaseInventoryQuantityByOrderAsync(It.IsAny<Order>()), Times.Never());
    }

    [Fact]
    public async Task PlaceOrderAsync_ProcessCouponReturnsInvalid_InvalidScenario()
    {
        // A
        var productOrderSaveList = new List<ProductOrderSave>()
        {
            ProductOrderBuilder.NewObject().WithProductId(123).SaveBuild()
        };
        var orderSave = OrderBuilder.NewObject().WithProductOrderSaveList(productOrderSaveList).WithCouponName("test").SaveBuild();

        _productOrderServiceMock.Setup(p => p.ProcessProductOrderListAsync(It.IsAny<List<ProductOrderSave>>(), It.IsAny<List<ProductOrder>>()))
            .ReturnsAsync(true);

        _couponServiceMock.Setup(c => c.ProcessDiscountAsync(It.IsAny<Order>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        // A
        await _ecommerceFacade.PlaceOrderAsync(orderSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _couponServiceMock.Verify(c => c.ProcessDiscountAsync(It.IsAny<Order>(), It.IsAny<string>()), Times.Once());
        _orderServiceMock.Verify(o => o.AddAsync(It.IsAny<Order>()), Times.Never());
        _inventoryServiceMock.Verify(i => i.DecreaseInventoryQuantityByOrderAsync(It.IsAny<Order>()), Times.Never());
    }
}

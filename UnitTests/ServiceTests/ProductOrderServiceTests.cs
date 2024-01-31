using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Interfaces.Settings;
using FacadePattern.API.Services;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServiceTests;
public sealed class ProductOrderServiceTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly ProductOrderService _productOrderService;

    public ProductOrderServiceTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _productOrderService = new ProductOrderService(_productServiceMock.Object, _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task ProcessProductOrderListAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var productOrderSaveList = new List<ProductOrderSave>()
        {
            ProductOrderBuilder.NewObject().WithQuantity(10).SaveBuild(),
            ProductOrderBuilder.NewObject().WithQuantity(1).SaveBuild()
        };
        var productOrderList = new List<ProductOrder>();

        var firstProduct = ProductBuilder.NewObject().WithQuantityAvailable(123).DomainBuild();
        var secondProduct = ProductBuilder.NewObject().WithQuantityAvailable(60).DomainBuild();
        _productServiceMock.SetupSequence(p => p.GetByIdRetunsDomainObjectAsync(It.IsAny<int>()))
            .ReturnsAsync(firstProduct)
            .ReturnsAsync(secondProduct);

        // A
        var processProductOrderListResult = await _productOrderService.ProcessProductOrderListAsync(productOrderSaveList, productOrderList);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

        Assert.True(processProductOrderListResult);
        Assert.Equal(productOrderList.Count, productOrderSaveList.Count);
    }

    [Fact]
    public async Task ProcessProductOrderListAsync_ProductDoesNotExist_ReturnsFalse()
    {
        // A
        var productOrderSaveList = new List<ProductOrderSave>()
        {
            ProductOrderBuilder.NewObject().SaveBuild(),
            ProductOrderBuilder.NewObject().SaveBuild()
        };
        var productOrderList = new List<ProductOrder>();

        var product = ProductBuilder.NewObject().DomainBuild();
        _productServiceMock.SetupSequence(p => p.GetByIdRetunsDomainObjectAsync(It.IsAny<int>()))
            .ReturnsAsync(product)
            .Returns(Task.FromResult<Product?>(null));

        // A
        var processProductOrderListResult = await _productOrderService.ProcessProductOrderListAsync(productOrderSaveList, productOrderList);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

        Assert.False(processProductOrderListResult);
    }

    [Fact]
    public async Task ProcessProductOrderListAsync_ProductOrderHasGreaterQuantityThanExistingProduct_ReturnsFalse()
    {
        // A
        var productOrderSaveList = new List<ProductOrderSave>()
        {
            ProductOrderBuilder.NewObject().WithQuantity(10).SaveBuild(),
            ProductOrderBuilder.NewObject().WithQuantity(1).SaveBuild(),
            ProductOrderBuilder.NewObject().WithQuantity(1000).SaveBuild()
        };
        var productOrderList = new List<ProductOrder>();

        var firstProduct = ProductBuilder.NewObject().WithQuantityAvailable(123).DomainBuild();
        var secondProduct = ProductBuilder.NewObject().WithQuantityAvailable(60).DomainBuild();
        var thirdProduct = ProductBuilder.NewObject().WithQuantityAvailable(50).DomainBuild();
        _productServiceMock.SetupSequence(p => p.GetByIdRetunsDomainObjectAsync(It.IsAny<int>()))
            .ReturnsAsync(firstProduct)
            .ReturnsAsync(secondProduct)
            .ReturnsAsync(thirdProduct);

        // A
        var processProductOrderListResult = await _productOrderService.ProcessProductOrderListAsync(productOrderSaveList, productOrderList);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

        Assert.False(processProductOrderListResult);
    }
}

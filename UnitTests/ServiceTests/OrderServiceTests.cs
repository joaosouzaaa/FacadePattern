using FacadePattern.API.DataTransferObjects.Order;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Interfaces.Repositories;
using FacadePattern.API.Services;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServiceTests;
public sealed class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IOrderMapper> _orderMapperMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderMapperMock = new Mock<IOrderMapper>();
        _orderService = new OrderService(_orderRepositoryMock.Object, _orderMapperMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario()
    {
        // A
        var order = OrderBuilder.NewObject().DomainBuild();

        _orderRepositoryMock.Setup(o => o.AddAsync(It.IsAny<Order>()));

        // A
        await _orderService.AddAsync(order);

        // A
        _orderRepositoryMock.Verify(o => o.AddAsync(It.IsAny<Order>()), Times.Once());
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsEntiyList()
    {
        // A
        var orderList = new List<Order>()
        {
            OrderBuilder.NewObject().DomainBuild()
        };
        _orderRepositoryMock.Setup(o => o.GetAllAsync())
            .ReturnsAsync(orderList);

        var orderResponseList = new List<OrderResponse>()
        {
            OrderBuilder.NewObject().ResponseBuild(),
            OrderBuilder.NewObject().ResponseBuild(),
            OrderBuilder.NewObject().ResponseBuild()
        };
        _orderMapperMock.Setup(o => o.DomainListToResponseList(It.IsAny<List<Order>>()))
            .Returns(orderResponseList);

        // A
        var orderResponseListResult = await _orderService.GetAllAsync();

        // A
        Assert.Equal(orderResponseListResult.Count, orderResponseList.Count);
    }
}

using FacadePattern.API.DataTransferObjects.ProductOrder;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Mappers;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public sealed class OrderMapperTests
{
    private readonly Mock<IProductOrderMapper> _productOrderMapperMock;
    private readonly OrderMapper _orderMapper;

    public OrderMapperTests()
    {
        _productOrderMapperMock = new Mock<IProductOrderMapper>();
        _orderMapper = new OrderMapper(_productOrderMapperMock.Object);
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario()
    {
        // A
        var productOrderList = new List<ProductOrder>()
        {
            ProductOrderBuilder.NewObject().DomainBuild(),
            ProductOrderBuilder.NewObject().DomainBuild(),
            ProductOrderBuilder.NewObject().DomainBuild()
        };

        var orderList = new List<Order>()
        {
            OrderBuilder.NewObject().WithProductOrderList(productOrderList).DomainBuild(),
            OrderBuilder.NewObject().WithProductOrderList(productOrderList).DomainBuild(),
            OrderBuilder.NewObject().WithProductOrderList(productOrderList).DomainBuild()
        };

        var productOrderResponseList = new List<ProductOrderResponse>()
        {
            ProductOrderBuilder.NewObject().ResponseBuild()
        };
        _productOrderMapperMock.SetupSequence(p => p.DomainListToResponseList(It.IsAny<List<ProductOrder>>()))
            .Returns(productOrderResponseList)
            .Returns(productOrderResponseList)
            .Returns(productOrderResponseList);

        // A
        var orderResponseListResult = _orderMapper.DomainListToResponseList(orderList);

        // A
        Assert.Equal(orderResponseListResult.Count, orderList.Count);
    }
}

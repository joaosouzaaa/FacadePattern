using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Repositories;
using FacadePattern.API.Services;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServiceTests;
public sealed class InventoryServiceTests
{
    private readonly Mock<IInventoryRepository> _inventoryRepositoryMock;
    private readonly InventoryService _inventoryService;

    public InventoryServiceTests()
    {
        _inventoryRepositoryMock = new Mock<IInventoryRepository>();
        _inventoryService = new InventoryService(_inventoryRepositoryMock.Object);
    }

    [Fact]
    public async Task DecreaseInventoryQuantityByOrderAsync_SuccessfulScenario()
    {
        // A
        const int firstProductQuantity = 10;
        const int secondProductQuantity = 90;
        var productOrderList = new List<ProductOrder>()
        {
            ProductOrderBuilder.NewObject().WithQuantity(firstProductQuantity).DomainBuild(),
            ProductOrderBuilder.NewObject().WithQuantity(secondProductQuantity).DomainBuild()
        };
        var order = OrderBuilder.NewObject().WithProductOrderList(productOrderList).DomainBuild();

        const int firstQuantity = 100;
        const int secondQuantity = 100;
        _inventoryRepositoryMock.SetupSequence(i => i.GetQuantityByProductIdAsync(It.IsAny<int>()))
            .ReturnsAsync(firstQuantity)
            .ReturnsAsync(secondQuantity);

        _inventoryRepositoryMock.Setup(i => i.UpdateQuantityByProductIdAsync(It.IsAny<int>(), It.IsAny<int>()));

        // A
        await _inventoryService.DecreaseInventoryQuantityByOrderAsync(order);

        // A
        _inventoryRepositoryMock.Verify(i => i.UpdateQuantityByProductIdAsync(It.IsAny<int>(), It.Is<int>(i => i == firstQuantity - firstProductQuantity)));
        _inventoryRepositoryMock.Verify(i => i.UpdateQuantityByProductIdAsync(It.IsAny<int>(), It.Is<int>(i => i == secondQuantity - secondProductQuantity)));
    }
}

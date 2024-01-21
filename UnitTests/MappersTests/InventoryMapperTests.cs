using FacadePattern.API.Mappers;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public sealed class InventoryMapperTests
{
    private readonly InventoryMapper _inventoryMapper;

    public InventoryMapperTests()
    {
        _inventoryMapper = new InventoryMapper();
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario()
    {
        // A
        var inventory = InventoryBuilder.NewObject().DomainBuild();

        // A
        var inventoryResponseResult = _inventoryMapper.DomainToResponse(inventory);

        // A
        Assert.Equal(inventoryResponseResult.Id, inventory.Id);
        Assert.Equal(inventoryResponseResult.Quantity, inventory.Quantity);
    }
}

using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Mappers;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public sealed class ProductMapperTests
{
    private readonly Mock<IInventoryMapper> _inventoryMapperMock;
    private readonly ProductMapper _productMapper;

    public ProductMapperTests()
    {
        _inventoryMapperMock = new Mock<IInventoryMapper>();
        _productMapper = new ProductMapper(_inventoryMapperMock.Object);
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario()
    {
        // A
        var productSave = ProductBuilder.NewObject().SaveBuild();

        // A
        var productResult = _productMapper.SaveToDomain(productSave);

        // A
        Assert.Equal(productResult.Name, productSave.Name);
        Assert.Equal(productResult.Price, productSave.Price);
        Assert.Equal(productResult.Inventory.Quantity, productSave.QuantityAvailable);
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario()
    {
        // A
        var productList = new List<Product>()
        {
            ProductBuilder.NewObject().DomainBuild(),
            ProductBuilder.NewObject().DomainBuild(),
            ProductBuilder.NewObject().DomainBuild()
        };

        var inventoryResponse = InventoryBuilder.NewObject().ResponseBuild();
        _inventoryMapperMock.SetupSequence(i => i.DomainToResponse(It.IsAny<Inventory>()))
            .Returns(inventoryResponse)
            .Returns(inventoryResponse)
            .Returns(inventoryResponse);

        // A
        var productResponseListResult = _productMapper.DomainListToResponseList(productList);

        // A
        Assert.Equal(productResponseListResult.Count, productList.Count);
    }
}

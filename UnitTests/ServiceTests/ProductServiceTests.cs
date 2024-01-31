using FacadePattern.API.DataTransferObjects.Product;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Interfaces.Repositories;
using FacadePattern.API.Interfaces.Settings;
using FacadePattern.API.Services;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServiceTests;
public sealed class ProductServiceTests 
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IProductMapper> _productMapperMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productMapperMock = new Mock<IProductMapper>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _productService = new ProductService(_productRepositoryMock.Object, _productMapperMock.Object, _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var productSave = ProductBuilder.NewObject().SaveBuild();

        var product = ProductBuilder.NewObject().DomainBuild();
        _productMapperMock.Setup(p => p.SaveToDomain(It.IsAny<ProductSave>()))
            .Returns(product);

        _productRepositoryMock.Setup(p => p.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _productService.AddAsync(productSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

        Assert.True(addResult);
    }

    [Theory]
    [MemberData(nameof(AddInvalidEntityParameters))]
    public async Task AddAsync_InvalidEntity_ReturnsFalse(ProductSave productSave)
    {
        // A
        _notificationHandlerMock.Setup(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()));

        // A
        var addResult = await _productService.AddAsync(productSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _productMapperMock.Verify(p => p.SaveToDomain(It.IsAny<ProductSave>()), Times.Never());
        _productRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Product>()), Times.Never());

        Assert.False(addResult);
    }

    public static IEnumerable<object[]> AddInvalidEntityParameters()
    {
        yield return new object[]
        {
            ProductBuilder.NewObject().WithName("").SaveBuild()
        };

        yield return new object[]
        {
            ProductBuilder.NewObject().WithName("a").SaveBuild()
        };

        yield return new object[]
        {
            ProductBuilder.NewObject().WithName(new string('a', 101)).SaveBuild()
        };

        yield return new object[]
        {
            ProductBuilder.NewObject().WithPrice(-1).SaveBuild()
        };

        yield return new object[]
        {
            ProductBuilder.NewObject().WithQuantityAvailable(-10).SaveBuild()
        };
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var id = 9;

        _productRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _productRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        // A
        var deleteResult = await _productService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

        Assert.True(deleteResult);
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var id = 9;

        _productRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // A
        var deleteResult = await _productService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _productRepositoryMock.Verify(p => p.DeleteAsync(It.IsAny<int>()), Times.Never());

        Assert.False(deleteResult);
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsEntityList()
    {
        // A
        var productList = new List<Product>()
        {
            ProductBuilder.NewObject().DomainBuild()
        };
        _productRepositoryMock.Setup(p => p.GetAllAsync())
            .ReturnsAsync(productList);

        var productResponseList = new List<ProductResponse>()
        {
            ProductBuilder.NewObject().ResponseBuild()
        };
        _productMapperMock.Setup(p => p.DomainListToResponseList(It.IsAny<List<Product>>()))
            .Returns(productResponseList);

        // A
        var productResponseListResult = await _productService.GetAllAsync();

        // A
        Assert.Equal(productResponseListResult.Count, productResponseList.Count);   
    }

    [Fact]
    public async Task GetByIdRetunsDomainObjectAsync_SuccessfulScenario_ReturnsEntity()
    {
        // A
        var id = 123;

        var product = ProductBuilder.NewObject().DomainBuild();
        _productRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(product);

        // A
        var productResult = await _productService.GetByIdRetunsDomainObjectAsync(id);

        // A
        Assert.NotNull(product);
    }
}

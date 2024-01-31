using FacadePattern.API.Entities;
using FacadePattern.API.Mappers;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public sealed class ProductOrderMapperTests
{
    private readonly ProductOrderMapper _productOrderMapper;

    public ProductOrderMapperTests()
    {
        _productOrderMapper = new ProductOrderMapper();
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

        // A
        var productOrderResponseListResult = _productOrderMapper.DomainListToResponseList(productOrderList);

        // A
        Assert.Equal(productOrderResponseListResult.Count, productOrderList.Count);
    }
}

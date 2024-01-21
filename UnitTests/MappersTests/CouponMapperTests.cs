using FacadePattern.API.Entities;
using FacadePattern.API.Mappers;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public sealed class CouponMapperTests
{
    private readonly CouponMapper _couponMapper;

    public CouponMapperTests()
    {
        _couponMapper = new CouponMapper();
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario()
    {
        // A
        var couponSave = CouponBuilder.NewObject().SaveBuild();

        // A
        var couponResult = _couponMapper.SaveToDomain(couponSave);

        // A
        Assert.Equal(couponResult.Name, couponSave.Name);
        Assert.Equal(couponResult.DiscountPorcentage, couponSave.DiscountPorcentage);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario()
    {
        // A
        var couponUpdate = CouponBuilder.NewObject().UpdateBuild();
        var couponResult = CouponBuilder.NewObject().DomainBuild();

        // A
        _couponMapper.UpdateToDomain(couponUpdate, couponResult);

        // A
        Assert.Equal(couponResult.Name, couponUpdate.Name);
        Assert.Equal(couponResult.DiscountPorcentage, couponUpdate.DiscountPorcentage);
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario()
    {
        // A
        var couponList = new List<Coupon>()
        {
            CouponBuilder.NewObject().DomainBuild(),
            CouponBuilder.NewObject().DomainBuild(),
            CouponBuilder.NewObject().DomainBuild()
        };

        // A
        var couponResponseListResult = _couponMapper.DomainListToResponseList(couponList);

        // A
        Assert.Equal(couponResponseListResult.Count, couponList.Count);
    }
}

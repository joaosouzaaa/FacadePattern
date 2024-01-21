using FacadePattern.API.DataTransferObjects.Coupon;
using FacadePattern.API.Entities;

namespace UnitTests.TestBuilders;
public sealed class CouponBuilder
{
    private double _discountPorcentage = 12.24f;
    private string _name = "name";

    public static CouponBuilder NewObject() =>
        new();

    public Coupon DomainBuild() =>
        new()
        {
            Id = 1,
            DiscountPorcentage = _discountPorcentage,
            Name = _name
        };

    public CouponSave SaveBuild() =>
        new(_name,
            _discountPorcentage);

    public CouponUpdate UpdateBuild() =>
        new(1,
            _name,
            _discountPorcentage);

    public CouponResponse ResponseBuild() =>
        new()
        {
            DiscountPorcentage = _discountPorcentage,
            Id = 123,
            Name = _name
        };

    public CouponBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public CouponBuilder WithDiscountPorcentage(double discountPorcentage)
    {
        _discountPorcentage = discountPorcentage;

        return this;
             
    }
}

namespace FacadePattern.API.DataTransferObjects.Coupon;

public sealed class CouponResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required double DiscountPorcentage { get; set; }
}

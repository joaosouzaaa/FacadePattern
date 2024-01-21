namespace FacadePattern.API.Entities;

public sealed class Coupon
{
    public int Id { get; set; }
    public required string Name { get; set; }    
    public required double DiscountPorcentage { get; set; }
}

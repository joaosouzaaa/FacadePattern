using FacadePattern.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacadePattern.API.Data.EntitiesMapping;

public sealed class CouponMapping : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("Coupons");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("varchar(100)");

        builder.Property(c => c.DiscountPorcentage)
            .IsRequired(true)
            .HasColumnName("discount_porcentage")
            .HasColumnType("decimal(5, 2)");
    }
}

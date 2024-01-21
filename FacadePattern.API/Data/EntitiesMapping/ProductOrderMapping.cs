using FacadePattern.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacadePattern.API.Data.EntitiesMapping;

public sealed class ProductOrderMapping : IEntityTypeConfiguration<ProductOrder>
{
    public void Configure(EntityTypeBuilder<ProductOrder> builder)
    {
        builder.ToTable("ProductOrders");

        builder.HasKey(p => p.ProductId);

        builder.Property(p => p.Quantity)
            .IsRequired(true)
            .HasColumnName("quantity")
            .HasColumnType("integer");

        builder.Property(p => p.TotalValue)
            .IsRequired(true)
            .HasColumnName("total_value")
            .HasColumnType("decimal(18, 2)");

        builder.HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .HasConstraintName("FK_ProductOrder_Product")
            .OnDelete(DeleteBehavior.Restrict);
    }
}

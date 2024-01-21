using FacadePattern.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacadePattern.API.Data.EntitiesMapping;

public sealed class InventoryMapping : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Quantity)
            .IsRequired(true)
            .HasColumnName("quantity")
            .HasColumnType("int");
    }
}

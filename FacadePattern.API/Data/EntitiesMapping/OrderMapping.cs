﻿using FacadePattern.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacadePattern.API.Data.EntitiesMapping;

public sealed class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.TotalValue)
            .IsRequired(true)
            .HasColumnName("total_value")
            .HasColumnType("decimal(18, 2)");

        builder.Property(o => o.CreationDate)
            .IsRequired(true)
            .HasColumnName("creation_date")
            .HasColumnType("datetime2");

        builder.HasMany(o => o.ProductsOrder)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId)
            .HasConstraintName("FK_Order_ProductOrder")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Infrastructure.Configurations
{
    public sealed class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder
                .HasKey(x => x.DiscountId);

            builder
                .Property(discount => discount.DiscountTypeId)
                .IsRequired()
                .HasColumnType("int");

            builder
                .Property(discount => discount.CategoryId)
                .HasColumnType("int");

            builder
             .Property(discount => discount.ItemId)
             .HasColumnType("int");

            builder
                .Property(discount => discount.DiscountTargetType)
                .IsRequired()
                .HasColumnType("int");

            builder 
                .Property(discount => discount.OrderId)
                .HasColumnType("uniqueidentifier");

            builder
                .Property(discount => discount.InvoiceId)
                .HasColumnType("uniqueidentifier");

            builder
             .Property(discount => discount.Comments);

            builder
                .HasOne(dt => dt.DiscountType)
                .WithMany(d => d.Discounts)
                .HasForeignKey(dt => dt.DiscountTypeId)
                .HasConstraintName("FK_Discount_DiscountType_DiscountTypeId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(c => c.Category)
                .WithMany(d => d.Discounts)
                .HasForeignKey(c => c.CategoryId)
                .HasConstraintName("FK_Discount_Category_CategoryId")
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(i => i.Item)
                .WithMany(d => d.Discounts)
                .HasForeignKey(i => i.ItemId)
                .HasConstraintName("FK_Discount_Item_ItemId")
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(o => o.OrderHeader)
                .WithMany(d => d.Discounts)
                .HasForeignKey(o => o.OrderId)
                .HasConstraintName("FK_Discount_Order_OrderId")
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(i => i.Invoice)
                .WithMany(d => d.Discounts)
                .HasForeignKey(i => i.InvoiceId)
                .HasConstraintName("FK_Discount_Invoice_InvoiceId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

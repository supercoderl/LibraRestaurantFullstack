using LibraRestaurant.Domain.Constants;
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
    public sealed class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder
                .HasKey(x => x.OrderLineId);

            builder
                .Property(orderLine => orderLine.OrderId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(orderLine => orderLine.ItemId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(orderLine => orderLine.Quantity)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(orderLine => orderLine.FoodPrice)
                .HasColumnType("money")
                .IsRequired();

            builder
                .Property(orderLine => orderLine.IsCanceled)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(orderLine => orderLine.CanceledTime)
                .HasColumnType("datetime");

            builder
                .Property(orderLine => orderLine.CanceledReason);

            builder
                .Property(orderLine => orderLine.CustomerReview);

            builder
                .Property(orderLine => orderLine.CustomerLike)
                .IsRequired()
                .HasColumnType("int");

            builder
                .HasOne(o => o.OrderHeader)
                .WithMany(ol => ol.OrderLines)
                .HasForeignKey(o => o.OrderId)
                .HasConstraintName("FK_OrderLine_Order_OrderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(i => i.Item)
                .WithMany(ol => ol.OrderLines)
                .HasForeignKey(i => i.ItemId)
                .HasConstraintName("FK_OrderLine_Item_ItemId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

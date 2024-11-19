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
    public sealed class OrderLogConfiguration : IEntityTypeConfiguration<OrderLog>
    {
        public void Configure(EntityTypeBuilder<OrderLog> builder)
        {
            builder
                .HasKey(x => x.LogId);

            builder
                .Property(orderLog => orderLog.OrderId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder
                .Property(orderLog => orderLog.ItemId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(orderLog => orderLog.ChangeType)
                .IsRequired();

            builder
                .Property(orderLog => orderLog.PreviousQuantity)
                .IsRequired()
                .HasColumnType("int");

            builder
                .Property(orderLog => orderLog.NewQuantity)
                .IsRequired()
                .HasColumnType("int");

            builder
                .Property(orderLog => orderLog.Time)
                .IsRequired()
                .HasColumnType("datetime");

            builder
                .HasOne(o => o.OrderHeader)
                .WithMany(ol => ol.OrderLogs)
                .HasForeignKey(o => o.OrderId)
                .HasConstraintName("FK_OrderLog_Order_OrderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(i => i.Item)
                .WithMany(ol => ol.OrderLogs)
                .HasForeignKey(i => i.ItemId)
                .HasConstraintName("FK_OrderLog_Item_ItemId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

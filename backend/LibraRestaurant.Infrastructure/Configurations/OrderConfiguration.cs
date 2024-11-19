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
    public sealed class OrderConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder
                .HasKey(x => x.OrderId);

            builder
                .Property(order => order.OrderNo)
                .IsRequired();

            builder
                .Property(order => order.StoreId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(order => order.PaymentMethodId)
                .HasColumnType("int")
                .IsRequired(false);

            builder
                .Property(order => order.PaymentTimeId)
                .HasColumnType("int")
                .IsRequired(false);

            builder
                .Property(order => order.ServantId)
                .HasColumnType("uniqueidentifier");

            builder
                .Property(order => order.CashierId)
                .HasColumnType("uniqueidentifier");

            builder
                .Property(order => order.CustomerNotes);

            builder
                .Property(order => order.ReservationId)
                .IsRequired();

            builder
                .Property(order => order.PriceCalculated)
                .IsRequired();

            builder
                .Property(order => order.PriceAdjustment);

            builder
                .Property(order => order.PriceAdjustmentReason);

            builder
                .Property(order => order.Subtotal)
                .IsRequired();

            builder
                .Property(order => order.Tax)
                .IsRequired();

            builder
                .Property(order => order.Total)
                .IsRequired();

            builder
                .Property(order => order.CustomerId)
                .HasColumnType("int");

            builder
                .Property(order => order.LatestStatus)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(order => order.LatestStatusUpdate)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(order => order.IsPaid)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(order => order.IsPreparationDelayed)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(order => order.DelayedTime);

            builder
                .Property(order => order.IsCanceled)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(order => order.CanceledTime);

            builder
                .Property(order => order.CanceledReason);

            builder
                .Property(order => order.IsReady)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(order => order.ReadyTime);

            builder
                .Property(order => order.IsCompleted)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(order => order.CompletedTime);

            builder
                .HasOne(r => r.Reservation)
                .WithMany(o => o.OrderHeaders)
                .HasForeignKey(o => o.ReservationId)
                .HasConstraintName("FK_OrderHeader_Reservation_ReservationId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.PaymentMethod)
                .WithMany(o => o.OrderHeaders)
                .HasForeignKey(p => p.PaymentMethodId)
                .HasConstraintName("FK_OrderHeader_PaymentMethod_PaymentMethodId")
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(s => s.Store)
                .WithMany(o => o.OrderHeaders)
                .HasForeignKey(s => s.StoreId)
                .HasConstraintName("FK_Order_Store_StoreId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(c => c.Customer)
                .WithOne(o => o.OrderHeader)
                .HasForeignKey<OrderHeader>(c => c.CustomerId)
                .HasConstraintName("FK_Order_Customer_CustomerId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

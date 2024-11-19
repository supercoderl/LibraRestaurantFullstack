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
    public sealed class PaymentHistoryConfiguration : IEntityTypeConfiguration<PaymentHistory>
    {
        public void Configure(EntityTypeBuilder<PaymentHistory> builder)
        {
            builder
                .HasKey(x => x.PaymentHistoryId);

            builder
                .Property(paymentHistory => paymentHistory.TransactionId)
                .IsRequired();

            builder
                .Property(paymentHistory => paymentHistory.OrderId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(paymentHistory => paymentHistory.PaymentMethodId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(paymentHistory => paymentHistory.Amount)
                .IsRequired()
                .HasColumnType("money");

            builder
                .Property(paymentHistory => paymentHistory.CurrencyId)
                .HasColumnType("int");

            builder
                .Property(paymentHistory => paymentHistory.Status)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(paymentHistory => paymentHistory.ResponseJSON);

            builder
                .Property(paymentHistory => paymentHistory.CallbackURL);

            builder
                .Property(paymentHistory => paymentHistory.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .HasOne(o => o.OrderHeader)
                .WithMany(ph => ph.PaymentHistories)
                .HasForeignKey(o => o.OrderId)
                .HasConstraintName("FK_PaymentHistory_Order_OrderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pm => pm.PaymentMethod)
                .WithMany(ph => ph.PaymentHistories)
                .HasForeignKey(pm => pm.PaymentMethodId)
                .HasConstraintName("FK_PaymentHistory_PaymentMethod_PaymentMethodId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

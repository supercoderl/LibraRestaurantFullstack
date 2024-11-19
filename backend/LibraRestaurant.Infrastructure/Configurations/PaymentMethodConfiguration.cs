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
    public sealed class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder
                .HasKey(x => x.PaymentMethodId);

            builder
                .Property(paymentMethod => paymentMethod.Name)
                .IsRequired();

            builder
                .Property(paymentMethod => paymentMethod.Description);

            builder
                .Property(paymentMethod => paymentMethod.Picture);

            builder
                .Property(paymentMethod => paymentMethod.IsActive)
                .IsRequired()
                .HasColumnType("bit");
        }
    }
}

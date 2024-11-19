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
    public sealed class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
    {
        public void Configure(EntityTypeBuilder<DiscountType> builder)
        {
            builder
                .HasKey(x => x.DiscountTypeId);

            builder
                .Property(role => role.Name)
                .IsRequired();

            builder
                .Property(role => role.Description);

            builder
                .Property(role => role.IsPercentage)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(role => role.Value)
                .IsRequired()
                .HasColumnType("money");

            builder
                .Property(role => role.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            builder
                .Property(role => role.StartTime)
                .IsRequired()
                .HasColumnType("datetime");

            builder
                .Property(role => role.EndTime)
                .IsRequired()
                .HasColumnType("datetime");

            builder
                .Property(role => role.CounponCode);

            builder
                .Property(role => role.MinOrderValue)
                .IsRequired()
                .HasColumnType("money");

            builder
                .Property(role => role.MinItemQuantity)
                .IsRequired()
                .HasColumnType("int");

            builder
                .Property(role => role.MaxDiscountValue)
                .IsRequired()
                .HasColumnType("money");
        }
    }
}

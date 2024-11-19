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
    public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(x => x.CustomerId);

            builder
                .Property(customer => customer.Name)
                .IsRequired();

            builder
                .Property(customer => customer.Phone)
                .IsRequired();

            builder
                .Property(customer => customer.Email);

            builder
                .Property(customer => customer.Address);

            builder
                .Property(customer => customer.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(customer => customer.UpdatedAt)
                .HasColumnType("datetime");
        }
    }
}

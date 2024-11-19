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
    public sealed class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder
                .HasKey(x => x.CurrencyId);

            builder
                .Property(currency => currency.Name)
                .IsRequired();

            builder
                .Property(currency => currency.Description);

            builder.HasData(new Currency(
                Ids.Seed.NumberId,
                "Việt Nam đồng",
                null));
        }
    }
}

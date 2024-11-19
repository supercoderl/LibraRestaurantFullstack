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
    public sealed class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder
                .HasKey(x => x.CityId);

            builder
                .Property(city => city.Name)
                .IsRequired();

            builder
                .Property(city => city.NameEn)
                .IsRequired();

            builder
                .Property(city => city.Fullname)
                .IsRequired();

            builder
                .Property(city => city.FullnameEn)
                .IsRequired();

            builder
                .Property(city => city.CodeName)
                .IsRequired();
        }
    }
}

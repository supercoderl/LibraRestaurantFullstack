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
    public sealed class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder
                .HasKey(x => x.DistrictId);

            builder
                .Property(district => district.Name)
                .IsRequired();

            builder
                .Property(district => district.NameEn)
                .IsRequired();

            builder
                .Property(district => district.Fullname)
                .IsRequired();

            builder
                .Property(district => district.FullnameEn)
                .IsRequired();

            builder
                .Property(district => district.CodeName)
                .IsRequired();

            builder
                .Property(district => district.CityId)
                .IsRequired()
                .HasColumnType("int");

            builder
                .HasOne(c => c.City)
                .WithMany(d => d.Districts)
                .HasForeignKey(c => c.CityId)
                .HasConstraintName("FK_District_City_CityId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

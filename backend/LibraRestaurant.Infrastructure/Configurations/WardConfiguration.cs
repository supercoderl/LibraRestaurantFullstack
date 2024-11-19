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
    public sealed class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> builder)
        {
            builder
                .HasKey(x => x.WardId);

            builder
                .Property(menu => menu.Name)
                .IsRequired();

            builder
                .Property(menu => menu.NameEn)
                .IsRequired();

            builder
                .Property(menu => menu.Fullname);

            builder
                .Property(menu => menu.FullnameEn)
                .IsRequired();

            builder
                .Property(menu => menu.CodeName)
                .IsRequired();

            builder
                .Property(menu => menu.DistrictId)
                .IsRequired()
                .HasColumnType("int");

            builder
                .HasOne(d => d.District)
                .WithMany(w => w.Wards)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Ward_District_DistrictId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

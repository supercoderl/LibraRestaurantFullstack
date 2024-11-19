using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Infrastructure.Configurations
{
    public sealed class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder
                .HasKey(x => x.StoreId);
                
            builder
                .Property(store => store.Name)
                .IsRequired();

            builder
                .Property(store => store.StoreId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(store => store.CityId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(store => store.DistrictId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(store => store.WardId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(store => store.TaxCode);

            builder
                .Property(store => store.Address)
                .IsRequired();

            builder
                .Property(store => store.GpsLocation);

            builder
                .Property(store => store.PostalCode);

            builder
                .Property(store => store.Phone);

            builder
                .Property(store => store.Fax);

            builder
                .Property(store => store.Email);

            builder
                .Property(store => store.Website);

            builder
                .Property(store => store.Logo);

            builder
                .Property(store => store.BankBranch);

            builder
                .Property(store => store.BankCode);

            builder
                .Property(store => store.BankAccount);

            builder
                .Property(store => store.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .HasOne(c => c.City)
                .WithMany(s => s.Stores)
                .HasForeignKey(c => c.CityId)
                .HasConstraintName("FK_Store_City_CityId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(d => d.District)
                .WithMany(s => s.Stores)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Store_District_DistrictId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(w => w.Ward)
                .WithMany(s => s.Stores)
                .HasForeignKey(w => w.WardId)
                .HasConstraintName("FK_Store_Ward_WardId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

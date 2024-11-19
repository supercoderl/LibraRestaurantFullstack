using LibraRestaurant.Domain.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Infrastructure.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder
                .HasKey(x => x.ReservationId);

            builder
                .Property(reservation => reservation.TableNumber)
                .IsRequired();

            builder
                .Property(reservation => reservation.Capacity)
                .IsRequired();

            builder
                .Property(reservation => reservation.Status)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(reservation => reservation.StoreId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder
               .Property(reservation => reservation.Description);

            builder
               .Property(reservation => reservation.ReservationTime)
               .HasColumnType("datetime");

            builder
               .Property(reservation => reservation.CustomerId)
               .HasColumnType("int");

            builder
               .Property(reservation => reservation.Code);

            builder
                .Property(reservation => reservation.CleaningTime)
                .HasColumnType("datetime");

            builder
                .HasOne(s => s.Store)
                .WithMany(r => r.Reservations)
                .HasForeignKey(s => s.StoreId)
                .HasConstraintName("FK_Reservation_Store_StoreId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(c => c.Customer)
                .WithOne(r => r.Reservation)
                .HasForeignKey<Reservation>(c => c.CustomerId)
                .HasConstraintName("FK_Reservation_Customer_CustomerId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

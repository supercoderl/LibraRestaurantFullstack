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
    public sealed class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder
                .HasKey(x => x.MenuId);

            builder
                .Property(menu => menu.Name)
                .IsRequired();

            builder
                .Property(menu => menu.StoreId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(menu => menu.Description);

            builder
                .Property(menu => menu.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .HasOne(s => s.Store)
                .WithMany(m => m.Menus)
                .HasForeignKey(s => s.StoreId)
                .HasConstraintName("FK_Menu_Store_StoreId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

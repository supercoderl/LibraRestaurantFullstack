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
    public sealed class CategoryItemConfiguration : IEntityTypeConfiguration<CategoryItem>
    {
        public void Configure(EntityTypeBuilder<CategoryItem> builder)
        {
            builder
                .HasKey(x => x.CategoryItemId);

            builder
                .Property(category => category.CategoryId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(category => category.ItemId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(category => category.Description);

            builder
                .HasOne(c => c.Category)
                .WithMany(ci => ci.CategoryItems)
                .HasForeignKey(c => c.CategoryId)
                .HasConstraintName("FK_CategoryItem_Category_CategoryId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(i => i.Item)
                .WithMany(ci => ci.CategoryItems)
                .HasForeignKey(i => i.ItemId)
                .HasConstraintName("FK_CategoryItem_Item_ItemId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

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
    public sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasKey(x => x.ReviewId);

            builder
                .Property(review => review.ItemId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(review => review.CustomerName)
                .IsRequired();

            builder
                .Property(review => review.CustomerEmail);

            builder
                .Property(review => review.Rating)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(review => review.Comment)
                .IsRequired();

            builder
                .Property(review => review.ReviewDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(review => review.Picture);

            builder
                .Property(review => review.LikeCounts)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(review => review.IsVerifiedPurchase)
                .HasColumnType("bit")
                .IsRequired();

            builder
                .Property(review => review.Response);

            builder
                .HasOne(i => i.Item)
                .WithMany(r => r.Reviews)
                .HasForeignKey(i => i.ItemId)
                .HasConstraintName("FK_Review_Item_ItemId")
                .OnDelete(DeleteBehavior.Cascade);  
        }
    }
}

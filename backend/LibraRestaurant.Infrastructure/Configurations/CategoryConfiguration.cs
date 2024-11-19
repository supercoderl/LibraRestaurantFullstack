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
    public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(x => x.CategoryId);

            builder
                .Property(category => category.Name)
                .IsRequired();

            builder
                .Property(category => category.Description);

            builder
                .Property(category => category.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(category => category.Picture);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        }
    }
}

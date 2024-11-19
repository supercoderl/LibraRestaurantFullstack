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
    public sealed class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder
                .HasKey(x => x.TokenId);

            builder
                .Property(token => token.OldToken)
                .IsRequired();

            builder
                .Property(token => token.EmployeeId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(token => token.RevokedAt)
                .HasColumnType("datetime");

            builder
                .Property(token => token.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder
                .Property(token => token.ExpireDate)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValue("2024-01-01");

            builder
                .HasOne(e => e.Employee)
                .WithMany(t => t.Tokens)
                .HasForeignKey(e => e.EmployeeId)
                .HasConstraintName("FK_Token_Employee_EmployeeId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

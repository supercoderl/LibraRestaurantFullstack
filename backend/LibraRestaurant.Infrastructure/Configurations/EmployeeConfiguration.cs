using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraRestaurant.Infrastructure.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .Property(employee => employee.Email)
            .IsRequired()
            .HasMaxLength(MaxLengths.Employee.Email);

        builder
            .Property(employee => employee.FirstName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Employee.FirstName);

        builder
            .Property(employee => employee.LastName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Employee.LastName);

        builder
            .Property(employee => employee.Mobile)
            .IsRequired()
            .HasMaxLength(MaxLengths.Employee.Mobile);

        builder
            .Property(employee => employee.Password)
            .IsRequired()
            .HasMaxLength(MaxLengths.Employee.Password);

        builder
            .HasOne(s => s.Store)
            .WithMany(e => e.Employees)
            .HasForeignKey(s => s.StoreId)
            .HasConstraintName("FK_Employee_Store_StoreId")
            .OnDelete(DeleteBehavior.SetNull);
    }
}
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
    public sealed class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
    {
        public void Configure(EntityTypeBuilder<EmployeeRole> builder)
        {
            builder
                .HasKey(x => x.EmployeeRoleId);

            builder
                .Property(employeeRole => employeeRole.RoleId)
                .HasColumnType("int")
                .IsRequired();

            builder
                .Property(employeeRole => employeeRole.EmployeeId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder
                .Property(employeeRole => employeeRole.AssignedDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(employeeRole => employeeRole.RevokedDate)
                .HasColumnType("datetime");

            builder
                .HasOne(r => r.Role)
                .WithMany(er => er.EmployeeRoles)
                .HasForeignKey(r => r.RoleId)
                .HasConstraintName("FK_EmployeeRole_Role_RoleId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Employee)
                .WithMany(er => er.EmployeeRoles)
                .HasForeignKey(e => e.EmployeeId)
                .HasConstraintName("FK_EmployeeRole_Employee_EmployeeId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

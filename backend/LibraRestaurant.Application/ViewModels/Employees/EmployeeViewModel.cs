using System;
using System.Collections.Generic;
using System.Linq;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Employees;

public sealed class EmployeeViewModel
{
    public Guid Id { get; set; }
    public Guid? StoreId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Mobile {  get; set; } = string.Empty;
    public UserStatus Status { get; set; }
    public string FullName {  get; set; } = string.Empty;
    public string? StoreName { get; set; }
    public List<int>? RoleIds { get; set; }

    public static EmployeeViewModel FromEmployee(Employee employee)
    {
        return new EmployeeViewModel
        {
            Id = employee.Id,
            StoreId = employee.StoreId,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Mobile = employee.Mobile,
            Status = employee.Status,
            FullName = string.Concat(employee.LastName, " ", employee.FirstName),
            StoreName = employee.Store?.Name,
            RoleIds = employee.EmployeeRoles?.Select(x => x.RoleId).ToList()
        };
    }
}
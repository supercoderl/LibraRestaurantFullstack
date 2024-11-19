using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;

namespace LibraRestaurant.Domain.Interfaces;

public interface IEmployee
{
    string Name { get; }
    Guid GetEmployeeId();
    List<UserRole> GetUserRoles();
    string GetEmployeeEmail();
}
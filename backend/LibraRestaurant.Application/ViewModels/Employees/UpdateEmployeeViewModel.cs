using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Employees;

public sealed record UpdateEmployeeViewModel(
    Guid Id,
    Guid? StoreId,
    string Email,
    string FirstName,
    string LastName,
    UserStatus Status,
    string Mobile);
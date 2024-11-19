using System;

namespace LibraRestaurant.Shared.Employees;

public sealed record EmployeeViewModel(
    Guid Id,
    Guid? StoreId,
    string Email,
    string FirstName,
    string LastName,
    string Mobile,
    bool IsDeleted);
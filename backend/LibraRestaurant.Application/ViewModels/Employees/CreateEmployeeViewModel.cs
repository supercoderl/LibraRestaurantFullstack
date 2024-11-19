using System;

namespace LibraRestaurant.Application.ViewModels.Employees;

public sealed record CreateEmployeeViewModel(
    Guid? StoreId,
    string Email,
    string FirstName,
    string Mobile,
    string LastName,
    DateTime RegisteredDate);
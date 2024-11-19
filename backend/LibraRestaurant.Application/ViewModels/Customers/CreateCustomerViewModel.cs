using System;

namespace LibraRestaurant.Application.ViewModels.Customers;

public sealed record CreateCustomerViewModel(
    string Name,
    string Phone,
    string? Email,
    string? Address);
using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Customers;

public sealed record UpdateCustomerViewModel(
    int CustomerId,
    string Name,
    string Phone,
    string? Email,
    string? Address);
using System;

namespace LibraRestaurant.Shared.Customer;

public sealed record CustomerViewModel(
    int CustomerId,
    string Name,
    string Phone,
    string? Email,
    string? Address);
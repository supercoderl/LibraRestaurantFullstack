using System;

namespace LibraRestaurant.Shared.PaymentMethods;

public sealed record PaymentMethodViewModel(
    int PaymentMethodId,
    string Name,
    string? Description,
    string? Picture,
    bool IsDeleted);
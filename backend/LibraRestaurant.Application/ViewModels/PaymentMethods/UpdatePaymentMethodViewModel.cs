using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.PaymentMethods;

public sealed record UpdatePaymentMethodViewModel(
    int PaymentMethodId,
    string Name,
    string? Description,
    string? Picture,
    bool IsActive);
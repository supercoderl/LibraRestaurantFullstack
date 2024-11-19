using System;

namespace LibraRestaurant.Application.ViewModels.PaymentMethods;

public sealed record CreatePaymentMethodViewModel(
    string Name,
    string? Description,
    string? Picture);
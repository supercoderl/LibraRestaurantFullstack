using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Currencies;

public sealed record UpdateCurrencyViewModel(
    int CurrencyId,
    string Name,
    string? Description);
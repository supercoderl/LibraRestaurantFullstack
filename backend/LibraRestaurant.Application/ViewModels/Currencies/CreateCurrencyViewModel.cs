using System;

namespace LibraRestaurant.Application.ViewModels.Currencies;

public sealed record CreateCurrencyViewModel(
    string Name,
    string Description);
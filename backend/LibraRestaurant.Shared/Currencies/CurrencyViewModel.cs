using System;

namespace LibraRestaurant.Shared.Currencies;

public sealed record CurrencyViewModel(
    int CurrencyId,
    string Name,
    string? Description,
    bool IsDeleted);
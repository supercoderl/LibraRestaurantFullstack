using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Currencies;

public sealed class CurrencyViewModel
{
    public int CurrencyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public static CurrencyViewModel FromCurrency(Currency currency)
    {
        return new CurrencyViewModel
        {
            CurrencyId = currency.CurrencyId,
            Name = currency.Name,
            Description = currency.Description,
        };
    }
}
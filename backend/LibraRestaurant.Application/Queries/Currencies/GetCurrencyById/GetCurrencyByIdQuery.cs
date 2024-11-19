using System;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Menus;
using MediatR;

namespace LibraRestaurant.Application.Queries.Currencies.GetCurrencyById;

public sealed record GetCurrencyByIdQuery(int Id) : IRequest<CurrencyViewModel?>;
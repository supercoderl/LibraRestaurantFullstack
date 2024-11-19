using System;
using LibraRestaurant.Application.ViewModels.Cities;
using LibraRestaurant.Application.ViewModels.Menus;
using MediatR;

namespace LibraRestaurant.Application.Queries.Cities.GetCityById;

public sealed record GetCityByIdQuery(int Id) : IRequest<CityViewModel?>;
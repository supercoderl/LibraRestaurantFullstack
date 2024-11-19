using System;
using LibraRestaurant.Application.ViewModels.Districts;
using LibraRestaurant.Application.ViewModels.Menus;
using MediatR;

namespace LibraRestaurant.Application.Queries.Districts.GetDistrictById;

public sealed record GetDistrictByIdQuery(int Id) : IRequest<DistrictViewModel?>;
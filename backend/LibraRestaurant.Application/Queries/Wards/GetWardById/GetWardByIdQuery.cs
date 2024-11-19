using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Wards;
using MediatR;

namespace LibraRestaurant.Application.Queries.Wards.GetWardById;

public sealed record GetWardByIdQuery(int Id) : IRequest<WardViewModel?>;
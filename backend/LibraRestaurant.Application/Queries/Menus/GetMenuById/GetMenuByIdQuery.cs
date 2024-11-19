using System;
using LibraRestaurant.Application.ViewModels.Menus;
using MediatR;

namespace LibraRestaurant.Application.Queries.Menus.GetMenuById;

public sealed record GetMenuByIdQuery(int Id) : IRequest<MenuViewModel?>;
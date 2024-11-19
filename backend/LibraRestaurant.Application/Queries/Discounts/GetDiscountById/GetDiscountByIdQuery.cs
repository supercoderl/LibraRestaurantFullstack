using System;
using LibraRestaurant.Application.ViewModels.Discounts;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using MediatR;

namespace LibraRestaurant.Application.Queries.Discounts.GetDiscountById;

public sealed record GetDiscountByIdQuery(int Id) : IRequest<DiscountViewModel?>;
using System;
using LibraRestaurant.Application.ViewModels.DiscountTypes;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using MediatR;

namespace LibraRestaurant.Application.Queries.DiscountTypes.GetDiscountTypeById;

public sealed record GetDiscountTypeByIdQuery(int Id) : IRequest<DiscountTypeViewModel?>;
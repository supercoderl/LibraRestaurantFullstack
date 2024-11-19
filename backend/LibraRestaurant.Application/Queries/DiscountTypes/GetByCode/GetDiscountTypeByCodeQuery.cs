using System;
using LibraRestaurant.Application.ViewModels.DiscountTypes;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using MediatR;

namespace LibraRestaurant.Application.Queries.DiscountTypes.GetDiscountTypeByCode;

public sealed record GetDiscountTypeByCodeQuery(string CounponCode) : IRequest<DiscountTypeViewModel?>;
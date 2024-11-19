using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Orders;
using MediatR;

namespace LibraRestaurant.Application.Queries.Orders.GetOrderById;

public sealed record GetOrderByIdQuery(Guid Id) : IRequest<OrderViewModel?>;
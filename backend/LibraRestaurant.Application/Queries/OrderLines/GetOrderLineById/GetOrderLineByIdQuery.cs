using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.OrderLines;
using MediatR;

namespace LibraRestaurant.Application.Queries.OrderLines.GetOrderLineById;

public sealed record GetOrderLineByIdQuery(int Id) : IRequest<OrderLineViewModel?>;
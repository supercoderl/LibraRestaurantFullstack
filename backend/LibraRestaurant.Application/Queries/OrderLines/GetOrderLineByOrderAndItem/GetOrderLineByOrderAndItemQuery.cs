using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.OrderLines;
using MediatR;

namespace LibraRestaurant.Application.Queries.OrderLines.GetOrderLineByOrderAndItem;

public sealed record GetOrderLineByOrderAndItemQuery(Guid OrderId, int ItemId) : IRequest<OrderLineViewModel?>;
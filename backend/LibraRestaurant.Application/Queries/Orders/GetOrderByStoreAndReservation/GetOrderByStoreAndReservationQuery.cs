using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Orders;
using MediatR;

namespace LibraRestaurant.Application.Queries.Orders.GetOrderByStoreAndReservation;

public sealed record GetOrderByStoreAndReservationQuery(Guid StoreId, int ReservationId) : IRequest<OrderViewModel?>;
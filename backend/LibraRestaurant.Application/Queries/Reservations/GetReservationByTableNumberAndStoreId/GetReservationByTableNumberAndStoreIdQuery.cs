using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reservations;
using MediatR;

namespace LibraRestaurant.Application.Queries.Reservations.GetReservationByTableNumberAndStoreId;

public sealed record GetReservationByTableNumberAndStoreIdQuery(int TableNumber, Guid StoreId) : IRequest<ReservationViewModel?>;
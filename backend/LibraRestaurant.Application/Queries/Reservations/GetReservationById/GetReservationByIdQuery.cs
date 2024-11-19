using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reservations;
using MediatR;

namespace LibraRestaurant.Application.Queries.Reservations.GetReservationById;

public sealed record GetReservationByIdQuery(int Id) : IRequest<ReservationViewModel?>;
using System;
using System.Collections.Generic;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Domain.Enums;
using MediatR;

namespace LibraRestaurant.Application.Queries.Reservations.GetReservationByStatus;

public sealed record GetReservationByStatusQuery(ReservationStatus Status) : IRequest<List<ReservationViewModel>>;
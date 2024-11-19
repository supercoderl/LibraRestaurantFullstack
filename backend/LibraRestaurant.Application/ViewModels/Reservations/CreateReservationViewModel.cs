using LibraRestaurant.Domain.Enums;
using System;

namespace LibraRestaurant.Application.ViewModels.Reservations;

public sealed record CreateReservationViewModel(
    int TableNumber,
    int Capacity,
    Guid StoreId,
    string? Description,
    DateTime? ReservationTime,
    int? CustomerId,
    DateTime? CleaningTime);
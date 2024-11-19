using System;

namespace LibraRestaurant.Shared.Reservations;

public sealed record ReservationViewModel(
    int ReservationId,
    int TableNumber,
    int Capacity,
    Guid StoreId,
    string? Description,
    DateTime? ReservationTime,
    int? CustomerId,
    string? Code,
    DateTime? CleaningTime,
    bool IsDeleted);
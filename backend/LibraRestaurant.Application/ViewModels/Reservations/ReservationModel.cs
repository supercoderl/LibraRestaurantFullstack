using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Reservations;

public sealed class ReservationViewModel
{
    public int ReservationId { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public ReservationStatus Status { get; set; }
    public Guid StoreId { get; set; }
    public string? Description { get; set; }
    public DateTime? ReservationTime { get; set; }
    public int? CustomerId { get; set; }
    public string? QRCode { get; set; }
    public DateTime? CleaningTime { get; set; }
    public string? StoreName { get; set; }
    public Guid? OrderId { get; set; }
    public string? CustomerPhone { get; set; }

    public static ReservationViewModel FromReservation(Reservation reservation, Guid? orderId)
    {
        return new ReservationViewModel
        {
            ReservationId = reservation.ReservationId,
            TableNumber = reservation.TableNumber,
            Capacity = reservation.Capacity,
            Status = reservation.Status,
            CustomerId = reservation.CustomerId,
            StoreId = reservation.StoreId,
            Description = reservation.Description,
            ReservationTime = reservation.ReservationTime,
            QRCode = reservation.Code,
            CleaningTime = reservation.CleaningTime,
            StoreName = reservation?.Store?.Name,
            OrderId = orderId,
            CustomerPhone = reservation?.Customer?.Phone
        };
    }
}
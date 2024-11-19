using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reservations.UpdateReservation
{
    public sealed class UpdateReservationCommand : CommandBase
    {
        private static readonly UpdateReservationCommandValidation s_validation = new();

        public int ReservationId { get; }
        public int TableNumber { get; }
        public int Capacity { get; }
        public ReservationStatus Status { get; }
        public Guid StoreId { get; }
        public string? Description { get; }
        public DateTime? ReservationTime { get; }
        public int? CustomerId { get; }
        public string? Code { get; }
        public DateTime? CleaningTime { get; }

        public UpdateReservationCommand(
            int reservationId,
            int tableNumber,
            int capacity,
            ReservationStatus status,
            Guid storeId,
            string? description,
            DateTime? reservationTime,
            int? customerId,
            string? code,
            DateTime? cleaningTime) : base(reservationId)
        {
            ReservationId = reservationId;
            TableNumber = tableNumber;
            Capacity = capacity;
            Status = status;
            StoreId = storeId;
            Description = description;
            ReservationTime = reservationTime;
            CustomerId = customerId;
            Code = code;
            CleaningTime = cleaningTime;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

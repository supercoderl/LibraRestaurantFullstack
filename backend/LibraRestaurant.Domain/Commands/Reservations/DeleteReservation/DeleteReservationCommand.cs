
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reservations.DeleteReservation
{
    public sealed class DeleteReservationCommand : CommandBase
    {
        private static readonly DeleteReservationCommandValidation s_validation = new();

        public int ReservationId { get; }

        public DeleteReservationCommand(int reservationId) : base(reservationId)
        {
            ReservationId = reservationId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

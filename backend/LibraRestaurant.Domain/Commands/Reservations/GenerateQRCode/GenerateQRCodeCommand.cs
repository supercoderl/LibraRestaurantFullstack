
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reservations.GenerateQRCode
{
    public sealed class GenerateQRCodeCommand : CommandBase
    {
        private static readonly GenerateQRCodeCommandValidation s_validation = new();

        public int ReservationId { get; }

        public GenerateQRCodeCommand(
            int reservationId
        ) : base(reservationId)
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

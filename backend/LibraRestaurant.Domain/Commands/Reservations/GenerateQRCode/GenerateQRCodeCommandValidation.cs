using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reservations.GenerateQRCode
{
    public sealed class GenerateQRCodeCommandValidation : AbstractValidator<GenerateQRCodeCommand>
    {
        public GenerateQRCodeCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.ReservationId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Reservation.EmptyId)
                .WithMessage("Id may not be empty");
        }
    }
}

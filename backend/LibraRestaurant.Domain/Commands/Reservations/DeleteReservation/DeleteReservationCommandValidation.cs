using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reservations.DeleteReservation
{
    public sealed class DeleteReservationCommandValidation : AbstractValidator<DeleteReservationCommand>
    {
        public DeleteReservationCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.ReservationId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Reservation.EmptyId)
                .WithMessage("Reservation id may not be empty");
        }
    }
}

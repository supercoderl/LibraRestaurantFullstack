using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reservations.UpdateReservationCustomer
{
    public sealed class UpdateReservationCustomerCommandValidation : AbstractValidator<UpdateReservationCustomerCommand>
    {
        public UpdateReservationCustomerCommandValidation()
        {
            AddRuleForReservationId();
        }

        private void AddRuleForReservationId()
        {
            RuleFor(cmd => cmd.ReservationId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Reservation.EmptyId)
                .WithMessage("Id may not be empty");
        }
    }
}

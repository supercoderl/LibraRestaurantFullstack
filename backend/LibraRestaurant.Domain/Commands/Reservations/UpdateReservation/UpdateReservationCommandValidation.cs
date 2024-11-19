using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reservations.UpdateReservation
{
    public sealed class UpdateReservationCommandValidation : AbstractValidator<UpdateReservationCommand>
    {
        public UpdateReservationCommandValidation()
        {
            AddRuleForReservationId();
            AddRuleForCapacity();
            AddRuleForStore();
            AddRuleForTableNumber();
        }

        private void AddRuleForTableNumber()
        {
            RuleFor(cmd => cmd.TableNumber)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Reservation.EmptyTableNumber)
                .WithMessage("Table number may not be empty");
        }

        private void AddRuleForReservationId()
        {
            RuleFor(cmd => cmd.ReservationId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Reservation.EmptyId)
                .WithMessage("Id may not be empty");
        }

        private void AddRuleForStore()
        {
            RuleFor(cmd => cmd.StoreId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Menu.EmptyStrore)
                .WithMessage("Store may not be empty");
        }

        private void AddRuleForCapacity()
        {
            RuleFor(cmd => cmd.Capacity)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Reservation.EmptyCapacity)
                .WithMessage("Capacity may not be empty");
        }
    }
}

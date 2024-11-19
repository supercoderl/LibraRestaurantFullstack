using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reservations.CreateReservation
{
    public sealed class CreateReservationCommandValidation : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidation()
        {
            AddRuleForStore();
            AddRuleForTableNumber();
            AddRuleForCapacity();
            AddRuleForStore();
        }

        private void AddRuleForTableNumber()
        {
            RuleFor(cmd => cmd.TableNumber)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Reservation.EmptyTableNumber)
                .WithMessage("Table number may not be empty");
        }

        private void AddRuleForCapacity()
        {
            RuleFor(cmd => cmd.Capacity)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Reservation.EmptyCapacity)
                .WithMessage("Capacity may not be empty");
        }

        private void AddRuleForStore()
        {
            RuleFor(cmd => cmd.StoreId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Reservation.EmptyStore)
                .WithMessage("Store id may not be empty");
        }
    }
}

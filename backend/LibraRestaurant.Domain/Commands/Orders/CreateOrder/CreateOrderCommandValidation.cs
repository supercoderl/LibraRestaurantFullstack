using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.CreateMenu;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Orders.CreateOrder
{
    public sealed class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation()
        {
            AddRuleForOrderNo();
            AddRuleForStore();
            AddRuleForReservation();
        }

        private void AddRuleForOrderNo()
        {
            RuleFor(cmd => cmd.OrderNo)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Menu.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForStore()
        {
            RuleFor(cmd => cmd.StoreId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Order.EmptyStore)
                .WithMessage("Store may not be empty");
        }

        private void AddRuleForReservation()
        {
            RuleFor(cmd => cmd.ReservationId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Order.EmptyReservation)
                .WithMessage("Reservation may not be empty");
        }
    }
}

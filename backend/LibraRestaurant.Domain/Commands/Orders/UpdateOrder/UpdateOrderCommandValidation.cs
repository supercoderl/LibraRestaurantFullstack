using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Orders.UpdateOrder
{
    public sealed class UpdateOrderCommandValidation : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidation()
        {
            AddRuleForOrderId();
            AddRuleForStore();
            AddRuleForReservation();
            AddRuleForPriceCalculated();
            AddRuleForSubtotal();
            AddRuleForTotal();
        }

        private void AddRuleForOrderId()
        {
            RuleFor(cmd => cmd.OrderId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Order.EmptyId)
                .WithMessage("Id may not be empty");
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

        private void AddRuleForPriceCalculated()
        {
            RuleFor(cmd => cmd.PriceCalculated)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Order.EmptyPriceCalculated)
                .WithMessage("Price calculated may not be empty");
        }

        private void AddRuleForSubtotal()
        {
            RuleFor(cmd => cmd.Subtotal)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Order.EmptySubtotal)
                .WithMessage("Subtotal may not be empty");
        }

        private void AddRuleForTotal()
        {
            RuleFor(cmd => cmd.Total)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Order.EmptyTotal)
                .WithMessage("Total may not be empty");
        }
    }
}

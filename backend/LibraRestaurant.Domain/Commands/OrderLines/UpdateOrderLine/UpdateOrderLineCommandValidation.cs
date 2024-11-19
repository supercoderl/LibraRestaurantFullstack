using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.OrderLines.UpdateOrderLine
{
    public sealed class UpdateOrderLineCommandValidation : AbstractValidator<UpdateOrderLineCommand>
    {
        public UpdateOrderLineCommandValidation()
        {
            AddRuleForId();
            AddRuleForOrder();
            AddRuleForItem();
            AddRuleForQuantity();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.OrderLineId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.OrderLine.EmptyId)
                .WithMessage("Id may not be empty");
        }

        private void AddRuleForOrder()
        {
            RuleFor(cmd => cmd.OrderId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.OrderLine.EmptyOrder)
                .WithMessage("Order id may not be empty");
        }

        private void AddRuleForItem()
        {
            RuleFor(cmd => cmd.ItemId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.OrderLine.EmptyItem)
                .WithMessage("Item id may not be empty");
        }

        private void AddRuleForQuantity()
        {
            RuleFor(cmd => cmd.Quantity)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.OrderLine.EmptyQuantity)
                .WithMessage("Quantity may not be empty");
        }
    }
}

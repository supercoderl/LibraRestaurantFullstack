using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.OrderLines.CreateOrderLine
{
    public sealed class CreateOrderLineCommandValidation : AbstractValidator<CreateOrderLineCommand>
    {
        public CreateOrderLineCommandValidation()
        {
            AddRuleForOrder();
            AddRuleForItem();
            AddRuleForQuantity();
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

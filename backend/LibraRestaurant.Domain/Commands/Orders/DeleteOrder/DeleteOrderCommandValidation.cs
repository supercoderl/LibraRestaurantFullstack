using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.DeleteMenu;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Orders.DeleteOrder
{
    public sealed class DeleteOrderCommandValidation : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.OrderId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Order.EmptyId)
                .WithMessage("Order id may not be empty");
        }
    }
}

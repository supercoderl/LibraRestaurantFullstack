using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.OrderLines.DeleteOrderLine
{
    public sealed class DeleteOrderLineCommandValidation : AbstractValidator<DeleteOrderLineCommand>
    {
        public DeleteOrderLineCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.OrderLineId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.OrderLine.EmptyId)
                .WithMessage("OrderLine id may not be empty");
        }
    }
}

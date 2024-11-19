using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.MenuItems.DeleteItem
{
    public sealed class DeleteItemCommandValidation : AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.ItemId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.MenuItem.EmptyId)
                .WithMessage("Item id may not be empty");
        }
    }
}

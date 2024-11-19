using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Stores.DeleteStore
{
    public sealed class DeleteStoreCommandValidation : AbstractValidator<DeleteStoreCommand>
    {
        public DeleteStoreCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.StoreId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Store.EmptyId)
                .WithMessage("Store id may not be empty");
        }
    }
}

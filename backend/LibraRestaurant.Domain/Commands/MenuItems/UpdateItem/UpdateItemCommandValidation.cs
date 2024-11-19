using FluentValidation;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.MenuItems.UpdateItem
{
    public sealed class UpdateItemCommandValidation : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidation()
        {
            AddRuleForItemId();
            AddRuleForTitle();
            AddRuleForSlug();
            AddRuleForSKU();
            AddRuleForPrice();
            AddRuleForQuantity();
        }

        private void AddRuleForTitle()
        {
            RuleFor(cmd => cmd.Title)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.MenuItem.EmptyTitle)
                .WithMessage("Title may not be empty");
        }

        private void AddRuleForItemId()
        {
            RuleFor(cmd => cmd.ItemId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.MenuItem.EmptyId)
                .WithMessage("Id may not be empty");
        }

        private void AddRuleForSlug()
        {
            RuleFor(cmd => cmd.Slug)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.MenuItem.EmptySlug)
                .WithMessage("Slug may not be empty")
                .MaximumLength(MaxLengths.MenuItem.Slug)
                .WithErrorCode(DomainErrorCodes.MenuItem.SlugExceedsMaxLength)
                .WithMessage($"Slug may not be longer than {MaxLengths.MenuItem.Slug} characters");
        }

        private void AddRuleForSKU()
        {
            RuleFor(cmd => cmd.SKU)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.MenuItem.EmptySlug)
                .WithMessage("Slug may not be empty")
                .MaximumLength(MaxLengths.MenuItem.SKU)
                .WithErrorCode(DomainErrorCodes.MenuItem.SlugExceedsMaxLength)
                .WithMessage($"SKU may not be longer than {MaxLengths.MenuItem.SKU} characters");
        }

        private void AddRuleForPrice()
        {
            RuleFor(cmd => cmd.Price)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.MenuItem.EmptyPrice)
                .WithMessage("Price may not be empty");
        }

        private void AddRuleForQuantity()
        {
            RuleFor(cmd => cmd.Quantity)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.MenuItem.EmptyQuantity)
                .WithMessage("Quantity may not be empty");
        }
    }
}

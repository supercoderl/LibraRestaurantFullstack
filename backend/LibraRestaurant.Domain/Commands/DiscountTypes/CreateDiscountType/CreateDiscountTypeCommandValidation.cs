﻿using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.DiscountTypes.CreateDiscountType
{
    public sealed class CreateDiscountTypeCommandValidation : AbstractValidator<CreateDiscountTypeCommand>
    {
        public CreateDiscountTypeCommandValidation()
        {
            AddRuleForName();
            AddRuleForValue();
            AddRuleForMinOrderValue();
            AddRuleForMinItemQuantity();
            AddRuleForMaxDiscountValue();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.DiscountType.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForValue()
        {
            RuleFor(cmd => cmd.Value)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.DiscountType.EmptyValue)
                .WithMessage("Value may not be empty");
        }

        private void AddRuleForMinOrderValue()
        {
            RuleFor(cmd => cmd.MinOrderValue)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.DiscountType.EmptyMinOrderValue)
                .WithMessage("Min order value may not be empty");
        }

        private void AddRuleForMinItemQuantity()
        {
            RuleFor(cmd => cmd.MinItemQuantity)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.DiscountType.EmptyMinItemQuantity)
                .WithMessage("Min item quantity may not be empty");
        }

        private void AddRuleForMaxDiscountValue()
        {
            RuleFor(cmd => cmd.MaxDiscountValue)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.DiscountType.EmptyMaxDiscountValue)
                .WithMessage("Max discount value may not be empty");
        }
    }
}
using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Stores.CreateStore
{
    public sealed class CreateStoreCommandValidation : AbstractValidator<CreateStoreCommand>
    {
        public CreateStoreCommandValidation()
        {
            AddRuleForName();
            AddRuleForCity();
            AddRuleForCity();
            AddRuleForDistrict();
            AddRuleForWard();
            AddRuleForAddress();
        }

        private void AddRuleForName()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Store.EmptyName)
                .WithMessage("Name may not be empty");
        }

        private void AddRuleForCity()
        {
            RuleFor(cmd => cmd.CityId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Store.EmptyCity)
                .WithMessage("City id may not be empty");
        }

        private void AddRuleForDistrict()
        {
            RuleFor(cmd => cmd.CityId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Store.EmptyDistrict)
                .WithMessage("District id may not be empty");
        }

        private void AddRuleForWard()
        {
            RuleFor(cmd => cmd.WardId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Store.EmptyWard)
                .WithMessage("Ward id may not be empty");
        }

        private void AddRuleForAddress()
        {
            RuleFor(cmd => cmd.Address)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Store.EmptyAddress)
                .WithMessage("Address may not be empty");
        }
    }
}

using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Currencies.UpdateCurrency
{
    public sealed class UpdateCurrencyCommand : CommandBase
    {
        private static readonly UpdateCurrencyCommandValidation s_validation = new();

        public int CurrencyId { get; }
        public string Name { get; }
        public string? Description { get; }

        public UpdateCurrencyCommand(
            int currencyId,
            string name,
            string? description) : base(currencyId)
        {
            CurrencyId = currencyId;
            Name = name;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

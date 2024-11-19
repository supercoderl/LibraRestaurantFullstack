using LibraRestaurant.Domain.Commands.Categories.DeleteCurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Currencies.DeleteCurrency
{
    public sealed class DeleteCurrencyCommand : CommandBase
    {
        private static readonly DeleteCurrencyCommandValidation s_validation = new();

        public int CurrencyId { get; }

        public DeleteCurrencyCommand(int currencyId) : base(currencyId)
        {
            CurrencyId = currencyId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

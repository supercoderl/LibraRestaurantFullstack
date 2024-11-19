using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Discounts.CreateDiscount
{
    public sealed class CreateDiscountCommandValidation : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidation()
        {

        }
    }
}

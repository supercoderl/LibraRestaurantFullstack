using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.CategoryItems.CreateCategoryItem
{
    public sealed class CreateCategoryItemCommandValidation : AbstractValidator<CreateCategoryItemCommand>
    {
        public CreateCategoryItemCommandValidation()
        {

        }
    }
}

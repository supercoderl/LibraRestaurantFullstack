
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.CategoryItems.DeleteCategoryItem
{
    public sealed class DeleteCategoryItemCommand : CommandBase
    {
        private static readonly DeleteCategoryItemCommandValidation s_validation = new();

        public int CategoryItemId { get; }

        public DeleteCategoryItemCommand(int categoryItemId) : base(categoryItemId)
        {
            CategoryItemId = categoryItemId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

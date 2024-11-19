
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Categories.DeleteCategory
{
    public sealed class DeleteCategoryCommand : CommandBase
    {
        private static readonly DeleteCategoryCommandValidation s_validation = new();

        public int CategoryId { get; }

        public DeleteCategoryCommand(int categoryId) : base(categoryId)
        {
            CategoryId = categoryId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

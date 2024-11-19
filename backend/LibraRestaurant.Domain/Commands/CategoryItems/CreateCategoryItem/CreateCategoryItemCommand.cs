
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.CategoryItems.CreateCategoryItem
{
    public sealed class CreateCategoryItemCommand : CommandBase
    {
        private static readonly CreateCategoryItemCommandValidation s_validation = new();

        public int CategoryItemId { get; }
        public int CategoryId { get; }
        public int ItemId { get; }
        public string? Description { get; }

        public CreateCategoryItemCommand(
            int categoryItemId,
            int categoryId,
            int itemId,
            string? description
        ) : base(categoryItemId)
        {
            CategoryItemId = categoryItemId;
            CategoryId = categoryId;
            ItemId = itemId;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

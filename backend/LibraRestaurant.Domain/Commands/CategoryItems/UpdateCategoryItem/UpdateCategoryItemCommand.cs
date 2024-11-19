using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.CategoryItems.UpdateCategoryItem
{
    public sealed class UpdateCategoryItemCommand : CommandBase
    {
        private static readonly UpdateCategoryItemCommandValidation s_validation = new();

        public int CategoryItemId { get; }
        public int CategoryId { get; }
        public int ItemId { get; }
        public string? Description { get; }

        public UpdateCategoryItemCommand(
            int categoryItemId,
            int categoryId,
            int itemId,
            string? description) : base(categoryItemId)
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.CategoryItems.UpsertCategoryItem
{
    public sealed class UpsertCategoryItemCommand : CommandBase
    {
        private static readonly UpsertCategoryItemCommandValidation s_validation = new();

        public int CategoryItemId { get; }
        public List<int> CategoryIds { get; }
        public int ItemId { get; }
        public string? Description { get; }

        public UpsertCategoryItemCommand(
            int categoryItemId,
            List<int> categoryIds,
            int itemId,
            string? description
        ) : base(categoryItemId)
        {
            CategoryItemId = categoryItemId;
            CategoryIds = categoryIds;
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

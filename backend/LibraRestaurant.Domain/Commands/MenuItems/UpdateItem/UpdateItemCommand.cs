
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.MenuItems.UpdateItem
{
    public sealed class UpdateItemCommand : CommandBase
    {
        private static readonly UpdateItemCommandValidation s_validation = new();

        public int ItemId { get; }
        public string Title { get; }
        public string Slug { get; }
        public string? Summary { get; }
        public string SKU { get; }
        public double Price { get; }
        public int Quantity { get; }
        public string? Recipe { get; }
        public string? Picture { get; }
        public string? Instruction { get; }
        public List<int> CategoryIds { get; }

        public UpdateItemCommand(
            int itemId,
            string title,
            string slug,
            string? summary,
            string sku,
            double price,
            int quantity,
            string? recipe,
            string? picture,
            string? instruction,
            List<int> categoryIds) : base(itemId)
        {
            ItemId = itemId;
            Title = title;
            Slug = slug;
            Summary = summary;
            SKU = sku;
            Price = price;
            Quantity = quantity;
            Recipe = recipe;
            Picture = picture;
            Instruction = instruction;
            CategoryIds = categoryIds;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

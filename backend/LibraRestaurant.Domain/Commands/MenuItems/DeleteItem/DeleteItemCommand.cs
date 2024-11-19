
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.MenuItems.DeleteItem
{
    public sealed class DeleteItemCommand : CommandBase
    {
        private static readonly DeleteItemCommandValidation s_validation = new();

        public int ItemId { get; }

        public DeleteItemCommand(int itemId) : base(itemId)
        {
            ItemId = itemId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

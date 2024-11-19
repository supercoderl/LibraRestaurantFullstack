
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Menus.DeleteMenu
{
    public sealed class DeleteMenuCommand : CommandBase
    {
        private static readonly DeleteMenuCommandValidation s_validation = new();

        public int MenuId { get; }

        public DeleteMenuCommand(int menuId) : base(menuId)
        {
            MenuId = menuId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

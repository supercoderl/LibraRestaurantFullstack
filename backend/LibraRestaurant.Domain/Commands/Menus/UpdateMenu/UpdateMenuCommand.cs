using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Menus.UpdateMenu
{
    public sealed class UpdateMenuCommand : CommandBase
    {
        private static readonly UpdateMenuCommandValidation s_validation = new();

        public int MenuId { get; }
        public string Name { get; }
        public Guid StoreId { get; }
        public string? Description { get; }
        public bool IsActive { get; }

        public UpdateMenuCommand(
            int menuId,
            string name,
            Guid storeId,
            string? description,
            bool isActive) : base(menuId)
        {
            MenuId = menuId;
            Name = name;
            StoreId = storeId;
            Description = description;
            IsActive = isActive;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

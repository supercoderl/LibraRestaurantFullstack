
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Menus.CreateMenu
{
    public sealed class CreateMenuCommand : CommandBase
    {
        private static readonly CreateMenuCommandValidation s_validation = new();

        public int MenuId { get; }
        public Guid StoreId { get; }
        public string Name { get; }
        public string? Description { get; }
        public bool IsActive { get; }

        public CreateMenuCommand(
            int menuId,
            Guid storeId,
            string name,
            string? description,
            bool isActive
        ) : base(menuId)
        {
            MenuId = menuId;
            StoreId = storeId;
            Name = name;
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

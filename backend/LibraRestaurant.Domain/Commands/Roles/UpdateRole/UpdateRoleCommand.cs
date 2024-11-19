using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Roles.UpdateRole
{
    public sealed class UpdateRoleCommand : CommandBase
    {
        private static readonly UpdateRoleCommandValidation s_validation = new();

        public int RoleId { get; }
        public string Name { get; }
        public string? Description { get; }

        public UpdateRoleCommand(
            int roleId,
            string name,
            string? description) : base(roleId)
        {
            RoleId = roleId;
            Name = name;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

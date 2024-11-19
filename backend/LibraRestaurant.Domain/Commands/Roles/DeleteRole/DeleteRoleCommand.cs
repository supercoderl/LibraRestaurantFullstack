
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Roles.DeleteRole
{
    public sealed class DeleteRoleCommand : CommandBase
    {
        private static readonly DeleteRoleCommandValidation s_validation = new();

        public int RoleId { get; }

        public DeleteRoleCommand(int roleId) : base(roleId)
        {
            RoleId = roleId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

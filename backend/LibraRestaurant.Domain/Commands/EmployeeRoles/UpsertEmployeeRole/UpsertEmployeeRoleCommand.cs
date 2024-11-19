
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.EmployeeRoles.UpsertEmployeeRole
{
    public sealed class UpsertEmployeeRoleCommand : CommandBase
    {
        private static readonly UpsertEmployeeRoleCommandValidation s_validation = new();

        public int EmployeeRoleId { get; }
        public List<int> RoleIds { get; }
        public Guid EmployeeId { get; }
        public DateTime AssigedDate { get; }
        public DateTime? RevokedDate { get; }

        public UpsertEmployeeRoleCommand(
            int employeeRoleId,
            List<int> roleIds,
            Guid employeeId,
            DateTime assigedDate,
            DateTime? revokedDate
        ) : base(employeeId)
        {
            EmployeeRoleId = employeeRoleId;
            RoleIds = roleIds;
            EmployeeId = employeeId;
            AssigedDate = assigedDate;
            RevokedDate = revokedDate;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

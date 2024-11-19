using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Tokens.UpdateToken
{
    public sealed class UpdateTokenCommand : CommandBase
    {
        private static readonly UpdateTokenCommandValidation s_validation = new();

        public int TokenId { get; }
        public string OldToken { get; }
        public Guid EmployeeId { get; }
        public bool IsActive { get; }
        public DateTime? RevokedAt { get; }
        public DateTime ExpireDate { get; }

        public UpdateTokenCommand(
            int tokenId,
            string oldToken,
            Guid employeeId,
            bool isActive,
            DateTime? revokedAt,
            DateTime expireDate) : base(tokenId)
        {
            TokenId = tokenId;
            OldToken = oldToken;
            EmployeeId = employeeId;
            IsActive = isActive;
            RevokedAt = revokedAt;
            ExpireDate = expireDate;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Tokens.CreateToken
{
    public sealed class CreateTokenCommand : CommandBase
    {
        private static readonly CreateTokenCommandValidation s_validation = new();

        public int TokenId { get; }
        public string OldToken { get; }
        public Guid EmployeeId { get; }

        public CreateTokenCommand(
            int tokenId,
            string oldToken,
            Guid employeeId
        ) : base(tokenId)
        {
            TokenId = tokenId;
            OldToken = oldToken;
            EmployeeId = employeeId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

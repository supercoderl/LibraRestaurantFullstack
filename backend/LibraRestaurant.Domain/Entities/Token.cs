using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Token : Entity
    {
        public int TokenId { get; private set; }
        public string OldToken {  get; private set; }
        public Guid EmployeeId { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public DateTime ExpireDate { get; private set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("Tokens")]
        public virtual Employee? Employee { get; set; }

        public Token(
            int tokenId,
            string oldToken,
            Guid employeeId,
            bool isActive,
            DateTime? revokedAt,
            DateTime expireDate
        ) : base(tokenId)
        {
            TokenId = tokenId;
            OldToken = oldToken;
            EmployeeId = employeeId;
            IsActive = isActive;
            RevokedAt = revokedAt;
            ExpireDate = expireDate;
        }

        public void SetOldToken( string oldToken )
        {
            OldToken = oldToken;
        }

        public void SetEmployee( Guid employeeId )
        {
            EmployeeId = employeeId;
        }

        public void SetIsActive( bool isActive )
        {
            IsActive = isActive;
        }

        public void SetRevokedAt( DateTime? revokedAt )
        {
            RevokedAt = revokedAt;
        }

        public void SetExpireDate(DateTime expireDate )
        {
            ExpireDate = expireDate;
        }
    }
}

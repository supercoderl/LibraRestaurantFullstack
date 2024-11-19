
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Stores.DeleteStore
{
    public sealed class DeleteStoreCommand : CommandBase
    {
        private static readonly DeleteStoreCommandValidation s_validation = new();

        public Guid StoreId { get; }

        public DeleteStoreCommand(Guid storeId) : base(storeId)
        {
            StoreId = storeId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

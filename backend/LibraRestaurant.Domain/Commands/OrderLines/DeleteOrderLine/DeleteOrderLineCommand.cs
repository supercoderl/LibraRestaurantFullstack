
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.OrderLines.DeleteOrderLine
{
    public sealed class DeleteOrderLineCommand : CommandBase
    {
        private static readonly DeleteOrderLineCommandValidation s_validation = new();

        public int OrderLineId { get; }

        public DeleteOrderLineCommand(int orderLineId) : base(orderLineId)
        {
            OrderLineId = orderLineId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

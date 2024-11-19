using LibraRestaurant.Domain.Commands.Menus.CreateMenu;
using LibraRestaurant.Domain.Commands.Menus.DeleteMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Orders.DeleteOrder
{
    public sealed class DeleteOrderCommand : CommandBase
    {
        private static readonly DeleteOrderCommandValidation s_validation = new();

        public Guid OrderId { get; }

        public DeleteOrderCommand(Guid orderId) : base(orderId)
        {
            OrderId = orderId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

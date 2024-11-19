using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Discounts.UpdateDiscount
{
    public sealed class UpdateDiscountCommand : CommandBase
    {
        private static readonly UpdateDiscountCommandValidation s_validation = new();

        public int DiscountId { get; }
        public int DiscountTypeId { get; }
        public DiscountTargetType DiscountTargetType { get; }
        public int? CategoryId { get; }
        public int? ItemId { get; }
        public Guid? OrderId { get; }
        public Guid? InvoiceId { get; }
        public string? Comments { get; }

        public UpdateDiscountCommand(
            int discountId,
            int discountTypeId,
            int? categoryId,
            DiscountTargetType discountTargetType,
            Guid? orderId,
            Guid? invoiceId,
            int? itemId,
            string? comment) : base(discountId)
        {
            DiscountId = discountId;
            DiscountTypeId = discountTypeId;
            CategoryId = categoryId;
            ItemId = itemId;
            DiscountTargetType = discountTargetType;
            OrderId = orderId;
            InvoiceId = invoiceId;
            Comments = comment;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

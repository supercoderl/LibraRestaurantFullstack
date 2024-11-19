
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Discounts.CreateDiscount
{
    public sealed class CreateDiscountCommand : CommandBase
    {
        private static readonly CreateDiscountCommandValidation s_validation = new();

        public int DiscountId { get; }
        public int DiscountTypeId { get; }
        public int? CategoryId { get; }
        public DiscountTargetType DiscountTargetType { get; }
        public Guid? OrderId { get; }
        public Guid? InvoiceId { get; }
        public int? ItemId { get; }
        public string? Comments { get; }

        public CreateDiscountCommand(
            int discountId,
            int discountTypeId,
            int? categoryId,
            int? itemId,
            DiscountTargetType discountTargetType,
            Guid? orderId,
            Guid? invoiceId,
            string? comments
        ) : base(discountId)
        {
            DiscountId = discountId;
            DiscountTypeId = discountTypeId;
            CategoryId = categoryId;
            ItemId = itemId;
            DiscountTargetType = discountTargetType;
            OrderId = orderId;
            InvoiceId = invoiceId;
            Comments = comments;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

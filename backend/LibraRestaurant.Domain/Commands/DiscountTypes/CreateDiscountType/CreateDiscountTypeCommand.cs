
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.DiscountTypes.CreateDiscountType
{
    public sealed class CreateDiscountTypeCommand : CommandBase
    {
        private static readonly CreateDiscountTypeCommandValidation s_validation = new();

        public int DiscountTypeId { get; }
        public string Name { get; }
        public string? Description { get; }
        public bool IsPercentage { get; }
        public double Value { get; }
        public DateTime CreatedAt { get; }
        public DateTime StartTime { get; }
        public DateTime? EndTime { get; }
        public string? CounponCode { get; }
        public double MinOrderValue { get; }
        public int MinItemQuantity { get; }
        public double MaxDiscountValue { get; }

        public CreateDiscountTypeCommand(
            int discountTypeId,
            string name,
            string? description,
            bool isPercentage,
            double value,
            DateTime createdAt,
            DateTime startTime,
            DateTime? endTime,
            string? counponCode,
            double minOrderValue,
            int minItemQuantity,
            double maxDiscountValue
        ) : base(discountTypeId)
        {
            DiscountTypeId = discountTypeId;
            Name = name;
            Description = description;
            IsPercentage = isPercentage;
            Value = value;
            CreatedAt = createdAt;
            StartTime = startTime;
            EndTime = endTime;
            CounponCode = counponCode;
            MinOrderValue = minOrderValue;
            MinItemQuantity = minItemQuantity;
            MaxDiscountValue = maxDiscountValue;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

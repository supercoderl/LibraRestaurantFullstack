using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class DiscountType : Entity
    {
        public int DiscountTypeId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsPercentage { get; private set; }
        public double Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public string? CounponCode { get; private set; }
        public double MinOrderValue { get; private set; }
        public int MinItemQuantity { get; private set; }
        public double MaxDiscountValue { get; private set; }

        [InverseProperty("DiscountType")]
        public virtual ICollection<Discount>? Discounts { get; set; } = new List<Discount>();

        public DiscountType(
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

        public void SetName( string name )
        {
            Name = name;
        }

        public void SetDescription( string? description )
        {
            Description = description;
        }

        public void SetIsPercentage( bool isPercentage )
        {
            IsPercentage = isPercentage;
        }

        public void SetValue( double value )
        {
            Value = value;
        }

        public void SetCreatedAt( DateTime createdAt )
        {
            CreatedAt = createdAt;
        }

        public void SetStartTime( DateTime startTime )
        {
            StartTime = startTime;
        }

        public void SetEndTime( DateTime? endTime )
        {
            EndTime = endTime;
        }

        public void SetCounponCode( string? counponCode )
        {
            CounponCode = counponCode;
        }

        public void SetMinOrderValue( double minOrderValue )
        {
            MinOrderValue = minOrderValue;
        }

        public void SetMinItemQuantity( int minItemQuantity )
        {
            MinItemQuantity = minItemQuantity;
        }

        public void SetMaxDiscountValue( double maxDiscountValue )
        {
            MaxDiscountValue = maxDiscountValue;
        }
    }
}

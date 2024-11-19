using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Discount : Entity
    {
        public int DiscountId { get; private set; }
        public int DiscountTypeId { get; private set; }
        public DiscountTargetType DiscountTargetType { get; private set; }
        public int? ItemId { get; private set; }
        public int? CategoryId { get; private set; }
        public Guid? OrderId { get; private set; }
        public Guid? InvoiceId { get; private set; }
        public string? Comments { get; private set; }

        [ForeignKey("DiscountTypeId")]
        [InverseProperty("Discounts")]
        public virtual DiscountType? DiscountType { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Discounts")]
        public virtual Category? Category { get; set; }

        [ForeignKey("ItemId")]
        [InverseProperty("Discounts")]
        public virtual MenuItem? Item { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("Discounts")]
        public virtual OrderHeader? OrderHeader { get; set; }

        [ForeignKey("InvoiceId")]
        [InverseProperty("Discounts")]
        public virtual Invoice? Invoice { get; set; }

        public Discount(
            int discountId,
            int discountTypeId,
            DiscountTargetType discountTargetType,
            int? categoryId,
            int? itemId,
            Guid? orderId,
            Guid? invoiceId,
            string? comments
        ) : base (discountId)
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

        public void SetDiscountTypeId( int discountTypeId )
        {
            DiscountTypeId = discountTypeId;
        }

        public void SetCategoryId( int? categoryId )
        {
            CategoryId = categoryId;
        }

        public void SetItemId( int? itemId )
        {
            ItemId = itemId;
        }

        public void SetDiscountTargetType( DiscountTargetType discountTargetType )
        {
            DiscountTargetType = discountTargetType;
        }

        public void SetOrderId( Guid? orderId )
        {
            OrderId = orderId;
        }

        public void SetInvoiceId( Guid? invoiceId )
        {
            InvoiceId = invoiceId;
        }

        public void SetComments( string? comments )
        {
            Comments = comments;
        }
    }
}

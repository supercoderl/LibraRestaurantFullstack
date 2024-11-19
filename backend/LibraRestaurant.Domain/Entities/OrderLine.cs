using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class OrderLine : Entity
    {
        public int OrderLineId { get; private set; }
        public Guid OrderId { get; private set; }
        public int ItemId { get; private set; }
        public int Quantity { get; private set; }
        public double FoodPrice { get; private set; }
        public bool IsCanceled { get; private set; }
        public DateTime? CanceledTime { get; private set; }
        public string? CanceledReason { get; private set; }
        public string? CustomerReview {  get; private set; }
        public CustomerLikeStatus CustomerLike { get; private set; }

        [ForeignKey("OrderId")]
        [InverseProperty("OrderLines")]
        public virtual OrderHeader? OrderHeader { get; set; }

        [ForeignKey("ItemId")]
        [InverseProperty("OrderLines")]
        public virtual MenuItem? Item { get; set; }

        public OrderLine(
            int orderLineId,
            Guid orderId,
            int itemId,
            int quantity,
            double foodPrice,
            bool isCanceled,
            DateTime? canceledTime,
            string? canceledReason,
            string? customerReview,
            CustomerLikeStatus customerLike
        ) : base(orderLineId)
        {
            OrderLineId = orderLineId;
            OrderId = orderId;
            ItemId = itemId;
            Quantity = quantity;
            FoodPrice = foodPrice;
            IsCanceled = isCanceled;
            CustomerReview = customerReview;
            CustomerLike = customerLike;
            CanceledTime = canceledTime;
            CanceledReason = canceledReason;
            CustomerReview = customerReview;
            CustomerLike = customerLike;
        }

        public void SetOrderId(Guid orderId)
        {
            OrderId = orderId;
        }

        public void SetItemId(int itemId) { ItemId = itemId; }

        public void SetQuantity(int quantity) { Quantity = quantity; }

        public void SetFoodPrice(double foodPrice) { FoodPrice = foodPrice; }

        public void SetIsCanceled(bool isCanceled) {  IsCanceled = isCanceled; }

        public void SetCanceledTime(DateTime? canceledTime) {  CanceledTime = canceledTime; }

        public void SetCanceledReason(string? canceledReason) { CanceledReason = canceledReason; }

        public void SetCustomerReview(string? customerReview) {  CustomerReview = customerReview; }

        public void SetCustomerLike(CustomerLikeStatus customerLike) { CustomerLike = customerLike; }
    }
}

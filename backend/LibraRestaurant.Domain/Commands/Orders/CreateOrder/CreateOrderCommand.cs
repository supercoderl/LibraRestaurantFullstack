using LibraRestaurant.Domain.Commands.Menus.CreateMenu;
using LibraRestaurant.Domain.Commands.OrderLines.CreateOrderLine;
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Orders.CreateOrder
{
    public sealed class CreateOrderCommand : CommandBase
    {
        private static readonly CreateOrderCommandValidation s_validation = new();

        public Guid OrderId { get; }
        public string OrderNo { get; }
        public Guid StoreId { get; }
        public int? PaymentMethodId { get; }
        public int? PaymentTimeId { get; }
        public Guid? ServantId { get; }
        public Guid? CashierId { get; }
        public string? CustomerNotes { get; }
        public int ReservationId { get; }
        public double PriceCalculated { get; }
        public double? PriceAdjustment { get; }
        public string? PriceAdjustmentReason { get; }
        public double Subtotal { get; }
        public double Tax { get; }
        public double Total { get; }
        public int? CustomerId { get; }
        public OrderStatus LatestStatus { get; }
        public DateTime LatestStatusUpdate { get; }
        public bool IsPaid { get; }
        public bool IsPreparationDelayed { get; }
        public DateTime? DelayedTime { get; }
        public bool IsCanceled { get; }
        public DateTime? CanceledTime { get; }
        public string? CanceledReason { get; }
        public bool IsReady { get; }
        public DateTime? ReadyTime { get; }
        public bool IsCompleted { get; }
        public DateTime? CompletedTime { get; }

        public List<CreateOrderLineCommand> OrderLines { get; }

        public CreateOrderCommand(
            Guid orderId,
            string orderNo,
            Guid storeId,
            int? paymentMethodId,
            int? paymentTimeId,
            Guid? servantId,
            Guid? cashierId,
            string? customerNotes,
            int reservationId,
            double priceCalculated,
            double? priceAdjustment,
            string? priceAdjustmentReason,
            double subtotal,
            double tax,
            double total,
            int? customerId,
            OrderStatus latestStatus,
            DateTime latestStatusUpdate,
            bool isPaid,
            bool isPreparationDelayed,
            DateTime? delayedTime,
            bool isCanceled,
            DateTime? caceledTime,
            string? canceledReason,
            bool isReady,
            DateTime? readyTime,
            bool isCompleted,
            DateTime? completedTime,
            List<CreateOrderLineCommand> orderLines
        ) : base(orderId)
        {
            OrderId = orderId;
            OrderNo = orderNo;
            StoreId = storeId;
            PaymentMethodId = paymentMethodId;
            PaymentTimeId = paymentTimeId;
            ServantId = servantId;
            CashierId = cashierId;
            CustomerNotes = customerNotes;
            ReservationId = reservationId;
            PriceCalculated = priceCalculated;
            PriceAdjustment = priceAdjustment;
            PriceAdjustmentReason = priceAdjustmentReason;
            Subtotal = subtotal;
            Tax = tax;
            Total = total;
            CustomerId = customerId;
            LatestStatus = latestStatus;
            LatestStatusUpdate = latestStatusUpdate;
            IsPaid = isPaid;
            IsPreparationDelayed = isPreparationDelayed;
            DelayedTime = delayedTime;
            IsCanceled = isCanceled;
            CanceledTime = caceledTime;
            CanceledReason = canceledReason;
            IsReady = isReady;
            ReadyTime = readyTime;
            IsCompleted = isCompleted;
            CompletedTime = completedTime;
            OrderLines = orderLines;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

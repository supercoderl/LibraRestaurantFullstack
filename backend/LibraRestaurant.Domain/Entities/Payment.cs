using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Payment : Entity
    {
        public Guid PaymentId { get; private set; }
        public string PaymentNo { get; private set; }
        public Guid OrderId { get; private set; }
        public double Amount { get; private set; }
        public DateTime PaymentTime { get; private set; }
        public bool IsCanceled { get; private set; }
        public DateTime? CanceledTime { get; private set; }
        public string? CanceledReason { get; private set; }

        public Payment(
            Guid paymentId,
            string paymentNo,
            Guid orderId,
            double amount,
            DateTime paymentTime,
            bool isCanceled,
            DateTime? canceledTime,
            string? canceledReason
        ) : base (paymentId)
        {
            PaymentId = paymentId;
            PaymentNo = paymentNo;
            OrderId = orderId;
            Amount = amount;
            PaymentTime = paymentTime;
            IsCanceled = isCanceled;
            CanceledTime = canceledTime;
            CanceledReason = canceledReason;
        }

        public void SetPaymentNo( string paymentNo )
        {
            PaymentNo = paymentNo;
        }

        public void SetOrderId( Guid orderId )
        {
            OrderId = orderId;
        }

        public void SetAmount( double amount )
        {
            Amount = amount;
        }

        public void SetPaymentTime( DateTime paymentTime )
        {
            PaymentTime = paymentTime;
        }

        public void SetIsCanceled(bool isCanceled)
        {
            IsCanceled = isCanceled;
        }

        public void SetCanceledTime(DateTime? canceledTime)
        {
            CanceledTime = canceledTime;
        }

        public void SetCanceledReason(string? reason)
        {
            CanceledReason = reason;
        }
    }
}

using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class PaymentHistory : Entity
    {
        public int PaymentHistoryId { get; private set; }
        public string TransactionId { get; private set; }
        public Guid OrderId { get; private set; }
        public int PaymentMethodId { get; private set; }
        public double Amount { get; private set; }
        public int? CurrencyId { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string? ResponseJSON { get; private set; }
        public string? CallbackURL { get; private set; }
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("OrderId")]
        [InverseProperty("PaymentHistories")]
        public virtual OrderHeader? OrderHeader { get; set; }

        [ForeignKey("PaymentMethodId")]
        [InverseProperty("PaymentHistories")]
        public virtual PaymentMethod? PaymentMethod { get; set; }

        public PaymentHistory(
            int paymentHistoryId,
            string transactionId,
            Guid orderId,
            int paymentMethodId,
            double amount,
            int? currencyId,
            string? responseJSON,
            string? callbackURL,
            DateTime createdAt,
            PaymentStatus status = PaymentStatus.Success
        ) : base(paymentHistoryId)
        {
            TransactionId = transactionId;
            OrderId = orderId;
            PaymentMethodId = paymentMethodId;
            Amount = amount;
            CurrencyId = currencyId;
            Status = status;
            ResponseJSON = responseJSON;
            CallbackURL = callbackURL;
            CreatedAt = createdAt;
        }

        public void SetTransaction(string transactionId)
        {
            TransactionId= transactionId;
        }

        public void SetOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public void SetPaymentMethod(int paymentMethodId)
        {
            PaymentMethodId = paymentMethodId;
        }

        public void SetAmount(double amount)
        {
            Amount = amount;
        }

        public void SetCurrency(int? currencyId)
        {
            CurrencyId = currencyId;
        }

        public void SetStatus(PaymentStatus status)
        {
            Status = status;
        }

        public void SetResponseJSON(string? responseJSON)
        {
            ResponseJSON = responseJSON;
        }

        public void SetCallbackURL(string? callbackURL)
        {
            CallbackURL = callbackURL;
        }

        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
    }
}

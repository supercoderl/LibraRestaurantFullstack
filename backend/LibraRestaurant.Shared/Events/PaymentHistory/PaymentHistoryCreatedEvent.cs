using System;

namespace LibraRestaurant.Shared.Events.PaymentHistory;

public sealed class PaymentHistoryCreatedEvent : DomainEvent
{
    public PaymentHistoryCreatedEvent(int paymentHistoryId) : base(paymentHistoryId)
    {
    }
}
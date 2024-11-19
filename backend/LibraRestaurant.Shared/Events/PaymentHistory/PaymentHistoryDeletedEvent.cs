using System;

namespace LibraRestaurant.Shared.Events.PaymentHistory;

public sealed class PaymentHistoryDeletedEvent : DomainEvent
{

    public PaymentHistoryDeletedEvent(int paymentHistoryId) : base(paymentHistoryId)
    {

    }
}
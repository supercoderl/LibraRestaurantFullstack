using System;

namespace LibraRestaurant.Shared.Events.PaymentMethod;

public sealed class PaymentMethodDeletedEvent : DomainEvent
{

    public PaymentMethodDeletedEvent(int paymentId) : base(paymentId)
    {

    }
}
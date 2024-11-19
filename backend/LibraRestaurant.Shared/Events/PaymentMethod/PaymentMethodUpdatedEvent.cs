using System;

namespace LibraRestaurant.Shared.Events.PaymentMethod;

public sealed class PaymentMethodUpdatedEvent : DomainEvent
{

    public PaymentMethodUpdatedEvent(int paymentId) : base(paymentId)
    {

    }
}
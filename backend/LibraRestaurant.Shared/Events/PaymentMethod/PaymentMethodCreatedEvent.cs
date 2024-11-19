using System;

namespace LibraRestaurant.Shared.Events.PaymentMethod;

public sealed class PaymentMethodCreatedEvent : DomainEvent
{
    public PaymentMethodCreatedEvent(int paymentId) : base(paymentId)
    {
    }
}
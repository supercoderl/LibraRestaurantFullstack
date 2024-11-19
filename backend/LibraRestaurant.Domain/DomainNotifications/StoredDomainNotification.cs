using System;
using LibraRestaurant.Domain.Notifications;

namespace LibraRestaurant.Domain.DomainNotifications;

public class StoredDomainNotification : DomainNotification
{
    public Guid Id { get; private set; }
    public string SerializedData { get; private set; } = string.Empty;
    public string Employee { get; private set; } = string.Empty;
    public string CorrelationId { get; private set; } = string.Empty;

    public StoredDomainNotification(
        DomainNotification domainNotification,
        string data,
        string employee,
        string correlationId) : base(
        domainNotification.Key,
        domainNotification.Value,
        domainNotification.Code,
        null,
        domainNotification.AggregateId)
    {
        Id = Guid.NewGuid();
        Employee = employee;
        SerializedData = data;
        CorrelationId = correlationId;
        MessageType = domainNotification.MessageType;
    }

    // EF Constructor
    protected StoredDomainNotification() : base(string.Empty, string.Empty, string.Empty)
    {
    }
}
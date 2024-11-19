using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.PaymentHistories;

public sealed class PaymentHistoryViewModel
{
    public int PaymentHistoryId { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public Guid OrderId { get; set; }
    public int PaymentMethodId { get; set; }
    public double Amount { get; set; }
    public int? CurrencyId { get; set; }
    public PaymentStatus Status { get; set; }
    public string? ResponseJSON { get; set; }
    public string? CallbackURL { get; set; }
    public DateTime CreatedAt { get; set; }

    public string? PaymentMethodName { get; set; }
    public string? CurrencyName { get; set; }

    public static PaymentHistoryViewModel FromPaymentHistory(PaymentHistory paymentHistory)
    {
        return new PaymentHistoryViewModel
        {
            PaymentHistoryId = paymentHistory.PaymentHistoryId,
            TransactionId = paymentHistory.TransactionId,
            OrderId = paymentHistory.OrderId,
            PaymentMethodId = paymentHistory.PaymentMethodId,
            Amount = paymentHistory.Amount,
            CurrencyId = paymentHistory.CurrencyId,
            Status = paymentHistory.Status,
            ResponseJSON = paymentHistory.ResponseJSON,
            CallbackURL = paymentHistory.CallbackURL,
            CreatedAt = paymentHistory.CreatedAt,
            PaymentMethodName = paymentHistory.PaymentMethod?.Name,
        };
    }
}
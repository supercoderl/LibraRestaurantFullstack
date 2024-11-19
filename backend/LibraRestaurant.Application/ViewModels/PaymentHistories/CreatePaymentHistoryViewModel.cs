using LibraRestaurant.Domain.Enums;
using System;

namespace LibraRestaurant.Application.ViewModels.PaymentHistorys;

public sealed record CreatePaymentHistoryViewModel(
    string TransactionId,
    Guid OrderId,
    int PaymentMethodId,
    double Amount,
    PaymentStatus Status,
    int? CurrencyId,
    string? ResponseJSON,
    string? CallbackURL);
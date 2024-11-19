using System;

namespace LibraRestaurant.Shared.PaymentHistorys;

public sealed record PaymentHistoryViewModel(
    int PaymentHistoryId,
    string TransactionId,
    Guid OrderId,
    int PaymentMethodId,
    double Amount,
    int? CurrencyId,
    string? ResponseJSON,
    string? CallbackURL,
    DateTime CreatedAt,
    bool IsDeleted);
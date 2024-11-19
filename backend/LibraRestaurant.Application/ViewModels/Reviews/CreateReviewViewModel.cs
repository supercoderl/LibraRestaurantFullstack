using System;

namespace LibraRestaurant.Application.ViewModels.Reviews;

public sealed record CreateReviewViewModel(
    int ReviewId,
    int ItemId,
    string CustomerName,
    string? CustomerEmail,
    int Rating,
    string Comment,
    string? Picture,
    bool IsVerifiedPurchase);
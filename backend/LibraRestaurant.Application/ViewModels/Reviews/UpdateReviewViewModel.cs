using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Reviews;

public sealed record UpdateReviewViewModel(
    int ReviewId,
    int ItemId,
    string CustomerName,
    string? CustomerEmail,
    int Rating,
    string Comment,
    string? Picture,
    int LikeCounts,
    bool IsVerifiedPurchase,
    string? Response);
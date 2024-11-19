using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Reviews;

public sealed class ReviewViewModel
{
    public int ReviewId { get; set; }
    public int ItemId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string? CustomerEmail { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    public string? Picture { get; set; }
    public int LikeCounts { get; set; }
    public bool IsVerifiedPurchase { get; set; }
    public string? Response { get; set; }

    public static ReviewViewModel FromReview(Review review)
    {
        return new ReviewViewModel
        {
            ReviewId = review.ReviewId,
            ItemId = review.ItemId,
            CustomerName = review.CustomerName,
            CustomerEmail = review.CustomerEmail,
            Rating = review.Rating,
            Comment = review.Comment,
            LikeCounts = review.LikeCounts,
            ReviewDate = review.ReviewDate,
            Picture = review.Picture,
            IsVerifiedPurchase = review.IsVerifiedPurchase,
            Response = review.Response,
        };
    }
}
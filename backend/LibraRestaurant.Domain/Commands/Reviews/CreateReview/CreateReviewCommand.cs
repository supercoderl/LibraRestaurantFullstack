
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reviews.CreateReview
{
    public sealed class CreateReviewCommand : CommandBase
    {
        private static readonly CreateReviewCommandValidation s_validation = new();

        public int ReviewId { get; }
        public int ItemId { get; }
        public string CustomerName { get; }
        public string? CustomerEmail { get; }
        public int Rating { get; }
        public string Comment { get;     }
        public string? Picture { get; }
        public bool IsVerifiedPurchase { get; }

        public CreateReviewCommand(
            int reviewId,
            int itemId,
            string customerName,
            string? customerEmail,
            int rating,
            string comment,
            string? picture,
            bool isVerifiedPurchase
        ) : base(reviewId)
        {
            ReviewId = reviewId;
            ItemId = itemId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            Rating = rating;
            Comment = comment;
            Picture = picture;
            IsVerifiedPurchase = isVerifiedPurchase;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

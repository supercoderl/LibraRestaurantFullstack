using LibraRestaurant.Domain.Commands.Menu.UpdateMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reviews.UpdateReview
{
    public sealed class UpdateReviewCommand : CommandBase
    {
        private static readonly UpdateReviewCommandValidation s_validation = new();

        public int ReviewId { get; }
        public int ItemId { get; }
        public string CustomerName { get; }
        public string? CustomerEmail { get; }
        public int Rating { get; }
        public string Comment { get; }
        public string? Picture { get; }
        public int LikeCounts { get; }
        public bool IsVerifiedPurchase { get; }
        public string? Response { get; }

        public UpdateReviewCommand(
            int reviewId,
            int itemId,
            string customerName,
            string? customerEmail,
            int rating,
            string comment,
            string? picture,
            int likeCounts,
            bool isVerifiedPurchase,
            string? response) : base(reviewId)
        {
            ReviewId = reviewId;
            ItemId = itemId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            Rating = rating;
            Comment = comment;
            Picture = picture;
            LikeCounts = likeCounts;
            IsVerifiedPurchase = isVerifiedPurchase;
            Response = response;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

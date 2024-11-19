
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reviews.DeleteReview
{
    public sealed class DeleteReviewCommand : CommandBase
    {
        private static readonly DeleteReviewCommandValidation s_validation = new();

        public int ReviewId { get; }

        public DeleteReviewCommand(int reviewId) : base(reviewId)
        {
            ReviewId = reviewId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

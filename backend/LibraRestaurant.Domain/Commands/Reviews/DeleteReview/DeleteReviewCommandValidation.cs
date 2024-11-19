using FluentValidation;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reviews.DeleteReview
{
    public sealed class DeleteReviewCommandValidation : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidation()
        {
            AddRuleForId();
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.ReviewId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Review.EmptyId)
                .WithMessage("Review id may not be empty");
        }
    }
}

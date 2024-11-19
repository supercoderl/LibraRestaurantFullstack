using FluentValidation;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Commands.Reviews.UpdateReview;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Commands.Reviews.UpdateReview
{
    public sealed class UpdateReviewCommandValidation : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidation()
        {
            AddRuleForComment();
            AddRuleForCustomerName();
            AddRuleForRating();
            AddRuleForId();
        }

        private void AddRuleForCustomerName()
        {
            RuleFor(cmd => cmd.CustomerName)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Review.EmptyName)
                .WithMessage("Customer name may not be empty");
        }

        private void AddRuleForRating()
        {
            RuleFor(cmd => cmd.Rating)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Review.EmptyRating)
                .WithMessage("Rating may not be empty");
        }

        private void AddRuleForComment()
        {
            RuleFor(cmd => cmd.Comment)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Review.EmptyComment)
                .WithMessage("Comment may not be empty");
        }

        private void AddRuleForId()
        {
            RuleFor(cmd => cmd.ReviewId)
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Review.EmptyId)
                .WithMessage("Id may not be empty");
        }
    }
}

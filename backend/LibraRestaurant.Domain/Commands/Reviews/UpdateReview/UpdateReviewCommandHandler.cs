
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.MenuItem;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Role;

namespace LibraRestaurant.Domain.Commands.Reviews.UpdateReview
{
    public sealed class UpdateReviewCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;

        public UpdateReviewCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReviewRepository reviewRepository) : base(bus, unitOfWork, notifications)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var review = await _reviewRepository.GetByIdAsync(request.ReviewId);

            if (review is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no review with Id {request.ReviewId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            review.SetItem(request.ItemId);
            review.SetCustomerName(request.CustomerName);
            review.SetCustomerEmail(request.CustomerEmail);
            review.SetRating(request.Rating);
            review.SetComment(request.Comment);
            review.SetPicture(request.Picture);
            review.SetLikeCounts(request.LikeCounts);
            review.SetIsVerifiedPurchase(request.IsVerifiedPurchase);
            review.SetResponse(request.Response);

            _reviewRepository.Update(review);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReviewUpdatedEvent(review.ReviewId));
            }
        }
    }
}

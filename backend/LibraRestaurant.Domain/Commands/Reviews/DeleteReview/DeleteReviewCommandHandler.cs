
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
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Reservation;
using LibraRestaurant.Shared.Events.Review;

namespace LibraRestaurant.Domain.Commands.Reviews.DeleteReview
{
    public sealed class DeleteReviewCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;

        public DeleteReviewCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReviewRepository reviewRepository) : base(bus, unitOfWork, notifications)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
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

            _reviewRepository.Remove(review);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReviewDeletedEvent(request.ReviewId));
            }
        }
    }
}

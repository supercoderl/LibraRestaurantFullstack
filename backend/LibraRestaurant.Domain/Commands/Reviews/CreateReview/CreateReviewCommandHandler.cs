
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.Reservation;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System;
using LibraRestaurant.Shared.Reservations;
using LibraRestaurant.Domain.Commands.Reviews.CreateReview;
using LibraRestaurant.Shared.Events.Review;

namespace LibraRestaurant.Domain.Commands.Reservations.CreateReservation
{
    public sealed class CreateReviewCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;

        public CreateReviewCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReviewRepository reviewRepository) : base(bus, unitOfWork, notifications)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var review = new Entities.Review(
                request.ReviewId,
                request.ItemId,
                request.CustomerName,
                request.CustomerEmail,
                request.Rating,
                request.Comment,
                DateTime.Now,
                request.Picture,
                0,
                request.IsVerifiedPurchase,
                null);

            _reviewRepository.Add(review);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReviewCreatedEvent(review.ReviewId));
            }
        }
    }
}

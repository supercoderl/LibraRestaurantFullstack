using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reviews;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Reviews.GetReviewById;

public sealed class GetReviewByIdQueryHandler :
    IRequestHandler<GetReviewByIdQuery, ReviewViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IReviewRepository _reviewRepository;

    public GetReviewByIdQueryHandler(IReviewRepository reviewRepository, IMediatorHandler bus)
    {
        _reviewRepository = reviewRepository;
        _bus = bus;
    }

    public async Task<ReviewViewModel?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByIdAsync(request.Id);

        if (review is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetReviewByIdQuery),
                    $"Review with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return ReviewViewModel.FromReview(review);
    }
}
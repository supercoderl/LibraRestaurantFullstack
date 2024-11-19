using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Reviews;
using LibraRestaurant.Application.Queries.Reviews.GetAll;
using LibraRestaurant.Domain.Commands.Reviews.CreateReview;
using LibraRestaurant.Domain.Commands.Reviews.DeleteReview;
using LibraRestaurant.Application.Queries.Reviews.GetReviewById;
using LibraRestaurant.Domain.Commands.Reviews.UpdateReview;

namespace LibraRestaurant.Application.Services
{
    public sealed class ReviewService : IReviewService
    {
        private readonly IMediatorHandler _bus;

        public ReviewService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<ReviewViewModel?> GetReviewByIdAsync(int reviewId)
        {
            return await _bus.QueryAsync(new GetReviewByIdQuery(reviewId));
        }

        public async Task<PagedResult<ReviewViewModel>> GetAllReviewsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            int? itemId = null,
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllReviewsQuery(query, includeDeleted, searchTerm, itemId, sortQuery));
        }

        public async Task<int> CreateReviewAsync(CreateReviewViewModel review)
        {
            await _bus.SendCommandAsync(new CreateReviewCommand(
                0,
                review.ItemId,
                review.CustomerName,
                review.CustomerEmail,
                review.Rating,
                review.Comment,
                review.Picture,
                review.IsVerifiedPurchase));

            return 0;
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            await _bus.SendCommandAsync(new DeleteReviewCommand(reviewId));
        }

        public async Task UpdateReviewAsync(UpdateReviewViewModel review)
        {
            await _bus.SendCommandAsync(new UpdateReviewCommand(
                review.ReviewId,
                review.ItemId,
                review.CustomerName,
                review.CustomerEmail,
                review.Rating,
                review.Comment,
                review.Picture,
                review.LikeCounts,
                review.IsVerifiedPurchase,
                review.Response
            ));
        }
    }
}

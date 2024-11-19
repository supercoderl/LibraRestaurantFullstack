using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Reviews;
using LibraRestaurant.Shared.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class ReviewsContext : IReviewsContext
    {
        private readonly ReviewsApi.ReviewsApiClient _client;

        public ReviewsContext(ReviewsApi.ReviewsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviewsByIds(IEnumerable<int> ids)
        {
            var request = new GetReviewsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Reviews.Select(review => new ReviewViewModel(
                review.Id,
                review.ItemId,
                review.CustomerName,
                review.CustomerEmail,
                review.Rating,
                review.Comment,
                review.Picture,
                review.LikeCounts,
                review.IsVerifiedPurchase,
                review.Response));
        }
    }
}

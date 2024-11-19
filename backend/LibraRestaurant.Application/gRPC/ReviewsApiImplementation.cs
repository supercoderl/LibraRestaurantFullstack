using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Reviews;
using LibraRestaurant.Proto.Roles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class ReviewsApiImplementation : ReviewsApi.ReviewsApiBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsApiImplementation(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public override async Task<GetReviewsByIdsResult> GetByIds(
            GetReviewsByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var reviews = await _reviewRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(review => idsAsIntegers.Contains(review.ReviewId))
                .Select(review => new GrpcReview
                {
                    Id = review.ReviewId,
                    ItemId = review.ReviewId,
                    CustomerName = review.CustomerName,
                    CustomerEmail = review.CustomerEmail,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    Picture = review.Picture,
                    LikeCounts = review.LikeCounts,
                    IsVerifiedPurchase = review.IsVerifiedPurchase,
                    Response = review.Response
                })
                .ToListAsync();

            var result = new GetReviewsByIdsResult();

            result.Reviews.AddRange(reviews);

            return result;
        }
    }
}

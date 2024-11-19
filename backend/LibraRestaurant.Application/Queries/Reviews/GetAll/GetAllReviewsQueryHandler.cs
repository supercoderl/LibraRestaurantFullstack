using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reviews;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Reviews.GetAll;

public sealed class GetAllReviewsQueryHandler :
    IRequestHandler<GetAllReviewsQuery, PagedResult<ReviewViewModel>>
{
    private readonly ISortingExpressionProvider<ReviewViewModel, Review> _sortingExpressionProvider;
    private readonly IReviewRepository _reviewRepository;

    public GetAllReviewsQueryHandler(
        IReviewRepository reviewRepository,
        ISortingExpressionProvider<ReviewViewModel, Review> sortingExpressionProvider)
    {
        _reviewRepository = reviewRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<ReviewViewModel>> Handle(
        GetAllReviewsQuery request,
        CancellationToken cancellationToken)
    {
        var reviewsQuery = _reviewRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            reviewsQuery = reviewsQuery.Where(review =>
                review.CustomerName.Contains(request.SearchTerm) ||
                review.Comment.Contains(request.SearchTerm));
        }

        if(request.itemId is not null)
        {
            reviewsQuery = reviewsQuery.Where(review => review.ItemId == request.itemId);
        }

        var totalCount = await reviewsQuery.CountAsync(cancellationToken);

        reviewsQuery = reviewsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var reviews = await reviewsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(review => ReviewViewModel.FromReview(review))
            .ToListAsync(cancellationToken);

        return new PagedResult<ReviewViewModel>(
            totalCount, reviews, request.Query.Page, request.Query.PageSize);
    }
}
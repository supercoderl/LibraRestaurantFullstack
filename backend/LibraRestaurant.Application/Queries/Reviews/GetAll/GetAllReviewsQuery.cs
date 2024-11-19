using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Reviews;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Reviews.GetAll;

public sealed record GetAllReviewsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    int? itemId = null,
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<ReviewViewModel>>;
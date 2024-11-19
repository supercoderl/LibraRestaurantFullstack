using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Discounts;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Discounts.GetAll;

public sealed record GetAllDiscountsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<DiscountViewModel>>;
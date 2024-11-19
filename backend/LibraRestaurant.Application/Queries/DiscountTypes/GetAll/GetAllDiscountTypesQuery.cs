using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.DiscountTypes;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.DiscountTypes.GetAll;

public sealed record GetAllDiscountTypesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<DiscountTypeViewModel>>;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Stores;
using MediatR;

namespace LibraRestaurant.Application.Queries.Stores.GetAll;

public sealed record GetAllStoresQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<StoreViewModel>>;
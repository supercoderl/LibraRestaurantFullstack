using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Wards;
using MediatR;

namespace LibraRestaurant.Application.Queries.Wards.GetAll;

public sealed record GetAllWardsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    bool IsAll,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<WardViewModel>>;
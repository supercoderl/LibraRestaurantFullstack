using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Categories;
using LibraRestaurant.Application.ViewModels.Sorting;
using MediatR;

namespace LibraRestaurant.Application.Queries.Categories.GetAll;

public sealed record GetAllCategoriesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    bool IsAll,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<CategoryViewModel>>;
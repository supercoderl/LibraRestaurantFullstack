using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Categories;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Categories.GetAll;

public sealed class GetAllCategoriesQueryHandler :
    IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryViewModel>>
{
    private readonly ISortingExpressionProvider<CategoryViewModel, Category> _sortingExpressionProvider;
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesQueryHandler(
        ICategoryRepository categoryRepository,
        ISortingExpressionProvider<CategoryViewModel, Category> sortingExpressionProvider)
    {
        _categoryRepository = categoryRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<CategoryViewModel>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categoriesQuery = _categoryRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            categoriesQuery = categoriesQuery.Where(menu =>
                menu.Name.Contains(request.SearchTerm) ||
                (menu.Description != null && menu.Description.Contains(request.SearchTerm)));
        }

        var totalCount = await categoriesQuery.CountAsync(cancellationToken);

        categoriesQuery = categoriesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        if (!request.IsAll)
        {
            categoriesQuery = categoriesQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize);
        }

        var categories = await categoriesQuery
            .Select(category => CategoryViewModel.FromCategory(category))
            .ToListAsync(cancellationToken);

        return new PagedResult<CategoryViewModel>(
            totalCount, categories, request.Query.Page, request.Query.PageSize);
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Menus.GetAll;

public sealed class GetAllMenusQueryHandler :
    IRequestHandler<GetAllMenusQuery, PagedResult<MenuViewModel>>
{
    private readonly ISortingExpressionProvider<MenuViewModel, Menu> _sortingExpressionProvider;
    private readonly IMenuRepository _menuRepository;

    public GetAllMenusQueryHandler(
        IMenuRepository menuRepository,
        ISortingExpressionProvider<MenuViewModel, Menu> sortingExpressionProvider)
    {
        _menuRepository = menuRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<MenuViewModel>> Handle(
        GetAllMenusQuery request,
        CancellationToken cancellationToken)
    {
        var menusQuery = _menuRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            menusQuery = menusQuery.Where(menu =>
                menu.Name.Contains(request.SearchTerm) ||
                (menu.Description != null && menu.Description.Contains(request.SearchTerm)));
        }

        var totalCount = await menusQuery.CountAsync(cancellationToken);

        menusQuery = menusQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var menus = await menusQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(menu => MenuViewModel.FromMenu(menu))
            .ToListAsync(cancellationToken);

        return new PagedResult<MenuViewModel>(
            totalCount, menus, request.Query.Page, request.Query.PageSize);
    }
}
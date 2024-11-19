using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Roles.GetAll;

public sealed class GetAllRolesQueryHandler :
    IRequestHandler<GetAllRolesQuery, PagedResult<RoleViewModel>>
{
    private readonly ISortingExpressionProvider<RoleViewModel, Role> _sortingExpressionProvider;
    private readonly IRoleRepository _roleRepository;

    public GetAllRolesQueryHandler(
        IRoleRepository roleRepository,
        ISortingExpressionProvider<RoleViewModel, Role> sortingExpressionProvider)
    {
        _roleRepository = roleRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<RoleViewModel>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken)
    {
        var rolesQuery = _roleRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            rolesQuery = rolesQuery.Where(role =>
                role.Name.Contains(request.SearchTerm) ||
                (role.Description != null && role.Description.Contains(request.SearchTerm)));
        }

        var totalCount = await rolesQuery.CountAsync(cancellationToken);

        rolesQuery = rolesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var roles = await rolesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(role => RoleViewModel.FromRole(role))
            .ToListAsync(cancellationToken);

        return new PagedResult<RoleViewModel>(
            totalCount, roles, request.Query.Page, request.Query.PageSize);
    }
}
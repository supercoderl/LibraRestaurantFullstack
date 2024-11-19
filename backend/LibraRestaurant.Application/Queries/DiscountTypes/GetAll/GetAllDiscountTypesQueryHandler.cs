using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.DiscountTypes;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.DiscountTypes.GetAll;

public sealed class GetAllDiscountTypesQueryHandler :
    IRequestHandler<GetAllDiscountTypesQuery, PagedResult<DiscountTypeViewModel>>
{
    private readonly ISortingExpressionProvider<DiscountTypeViewModel, DiscountType> _sortingExpressionProvider;
    private readonly IDiscountTypeRepository _discountTypeRepository;

    public GetAllDiscountTypesQueryHandler(
        IDiscountTypeRepository discountTypeRepository,
        ISortingExpressionProvider<DiscountTypeViewModel, DiscountType> sortingExpressionProvider)
    {
        _discountTypeRepository = discountTypeRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<DiscountTypeViewModel>> Handle(
        GetAllDiscountTypesQuery request,
        CancellationToken cancellationToken)
    {
        var discountTypesQuery = _discountTypeRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            discountTypesQuery = discountTypesQuery.Where(role =>
                role.Name.Contains(request.SearchTerm) ||
                (role.Description != null && role.Description.Contains(request.SearchTerm)));
        }

        var totalCount = await discountTypesQuery.CountAsync(cancellationToken);

        discountTypesQuery = discountTypesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var discountTypes = await discountTypesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(discountType => DiscountTypeViewModel.FromDiscountType(discountType))
            .ToListAsync(cancellationToken);

        return new PagedResult<DiscountTypeViewModel>(
            totalCount, discountTypes, request.Query.Page, request.Query.PageSize);
    }
}
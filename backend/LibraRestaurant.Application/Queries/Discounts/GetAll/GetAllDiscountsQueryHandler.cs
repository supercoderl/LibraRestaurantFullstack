using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Discounts;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Discounts.GetAll;

public sealed class GetAllDiscountsQueryHandler :
    IRequestHandler<GetAllDiscountsQuery, PagedResult<DiscountViewModel>>
{
    private readonly ISortingExpressionProvider<DiscountViewModel, Discount> _sortingExpressionProvider;
    private readonly IDiscountRepository _discountRepository;

    public GetAllDiscountsQueryHandler(
        IDiscountRepository discountRepository,
        ISortingExpressionProvider<DiscountViewModel, Discount> sortingExpressionProvider)
    {
        _discountRepository = discountRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<DiscountViewModel>> Handle(
        GetAllDiscountsQuery request,
        CancellationToken cancellationToken)
    {
        var discountsQuery = _discountRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {

        }

        var totalCount = await discountsQuery.CountAsync(cancellationToken);

        discountsQuery = discountsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var discounts = await discountsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(discount => DiscountViewModel.FromDiscount(discount))
            .ToListAsync(cancellationToken);

        return new PagedResult<DiscountViewModel>(
            totalCount, discounts, request.Query.Page, request.Query.PageSize);
    }
}
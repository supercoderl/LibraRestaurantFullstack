using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Currencies.GetAll;

public sealed class GetAllCurrenciesQueryHandler :
    IRequestHandler<GetAllCurrenciesQuery, PagedResult<CurrencyViewModel>>
{
    private readonly ISortingExpressionProvider<CurrencyViewModel, Currency> _sortingExpressionProvider;
    private readonly ICurrencyRepository _currencyRepository;

    public GetAllCurrenciesQueryHandler(
        ICurrencyRepository currencyRepository,
        ISortingExpressionProvider<CurrencyViewModel, Currency> sortingExpressionProvider)
    {
        _currencyRepository = currencyRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<CurrencyViewModel>> Handle(
        GetAllCurrenciesQuery request,
        CancellationToken cancellationToken)
    {
        var currenciesQuery = _currencyRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            currenciesQuery = currenciesQuery.Where(menu =>
                menu.Name.Contains(request.SearchTerm) ||
                (menu.Description != null && menu.Description.Contains(request.SearchTerm)));
        }

        var totalCount = await currenciesQuery.CountAsync(cancellationToken);

        currenciesQuery = currenciesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var currencies = await currenciesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(currency => CurrencyViewModel.FromCurrency(currency))
            .ToListAsync(cancellationToken);

        return new PagedResult<CurrencyViewModel>(
            totalCount, currencies, request.Query.Page, request.Query.PageSize);
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Stores;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Stores.GetAll;

public sealed class GetAllStoresQueryHandler :
    IRequestHandler<GetAllStoresQuery, PagedResult<StoreViewModel>>
{
    private readonly ISortingExpressionProvider<StoreViewModel, Store> _sortingExpressionProvider;
    private readonly IStoreRepository _storeRepository;

    public GetAllStoresQueryHandler(
        IStoreRepository storeRepository,
        ISortingExpressionProvider<StoreViewModel, Store> sortingExpressionProvider)
    {
        _storeRepository = storeRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<StoreViewModel>> Handle(
        GetAllStoresQuery request,
        CancellationToken cancellationToken)
    {
        var storesQuery = _storeRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            storesQuery = storesQuery.Where(store =>
                store.Name.Contains(request.SearchTerm));
        }

        var totalCount = await storesQuery.CountAsync(cancellationToken);

        storesQuery = storesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var stores = await storesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(store => StoreViewModel.FromMenu(store))
            .ToListAsync(cancellationToken);

        return new PagedResult<StoreViewModel>(
            totalCount, stores, request.Query.Page, request.Query.PageSize);
    }
}

using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.MenuItems;
using LibraRestaurant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LibraRestaurant.Application.Extensions;

namespace LibraRestaurant.Application.Queries.MenuItems.GetAll
{
    public sealed class GetAllItemsQueryHandler :
        IRequestHandler<GetAllItemsQuery, PagedResult<ItemViewModel>>
    {
        private readonly ISortingExpressionProvider<ItemViewModel, MenuItem> _sortingExpressionProvider;
        private readonly IMenuItemRepository _itemRepository;

        public GetAllItemsQueryHandler(
            IMenuItemRepository itemRepository,
            ISortingExpressionProvider<ItemViewModel, MenuItem> sortingExpressionProvider)
        {
            _itemRepository = itemRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PagedResult<ItemViewModel>> Handle(
            GetAllItemsQuery request,
            CancellationToken cancellationToken)
        {
            var itemsQuery = _itemRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => request.IncludeDeleted || !x.Deleted);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                itemsQuery = itemsQuery.Where(item =>
                    item.Title.Contains(request.SearchTerm) ||
                    item.Slug.Contains(request.SearchTerm) ||
                    (!string.IsNullOrEmpty(item.Summary) && item.Summary.Contains(request.SearchTerm)) ||
                    item.SKU.Contains(request.SearchTerm) ||
                    (!string.IsNullOrEmpty(item.Recipe) && item.Recipe.Contains(request.SearchTerm)) ||
                    (!string.IsNullOrEmpty(item.Instruction) && item.Instruction.Contains(request.SearchTerm))
                );
            }

            if(request.CategoryId != -1)
            {
                itemsQuery = itemsQuery.Where(item => item.CategoryItems != null && item.CategoryItems.Where(categoryItem => categoryItem.CategoryId == request.CategoryId).Any());
            }

            var totalCount = await itemsQuery.CountAsync(cancellationToken);

            itemsQuery = itemsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            var items = await itemsQuery
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Select(item => ItemViewModel.FromItem(item))
                .ToListAsync(cancellationToken);

            return new PagedResult<ItemViewModel>(
                totalCount, items, request.Query.Page, request.Query.PageSize);
        }
    }
}

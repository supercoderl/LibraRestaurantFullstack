using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.MenuItems;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class ItemsApiImplementation : ItemsApi.ItemsApiBase
    {
        private readonly IMenuItemRepository _itemRepository;

        public ItemsApiImplementation(IMenuItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public override async Task<GetItemsByIdsResult> GetByIds(
            GetItemsByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var items = await _itemRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(item => idsAsIntegers.Contains(item.ItemId))
                .Select(item => new GrpcMenuItem
                {
                    Id = item.ItemId,
                    Title = item.Title,
                    Slug = item.Slug,
                    Summary = item.Summary,
                    Sku = item.SKU,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Recipe = item.Recipe,
                    Instruction = item.Instruction,
                    Picture = item.Picture,
                    IsDeleted = item.Deleted
                })
                .ToListAsync();

            var result = new GetItemsByIdsResult();

            result.Items.AddRange(items);

            return result;
        }
    }
}

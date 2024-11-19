using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Discounts;
using LibraRestaurant.Application.Queries.Discounts.GetDiscountById;
using LibraRestaurant.Application.Queries.Discounts.GetAll;
using LibraRestaurant.Domain.Commands.Discounts.CreateDiscount;
using LibraRestaurant.Domain.Commands.Discounts.UpdateDiscount;
using LibraRestaurant.Domain.Commands.Discounts.DeleteDiscount;

namespace LibraRestaurant.Application.Services
{
    public sealed class DiscountService : IDiscountService
    {
        private readonly IMediatorHandler _bus;

        public DiscountService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<DiscountViewModel?> GetDiscountByIdAsync(int discountId)
        {
            return await _bus.QueryAsync(new GetDiscountByIdQuery(discountId));
        }

        public async Task<PagedResult<DiscountViewModel>> GetAllDiscountsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllDiscountsQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreateDiscountAsync(CreateDiscountViewModel discount)
        {
            await _bus.SendCommandAsync(new CreateDiscountCommand(
                0,
                discount.DiscountTypeId,
                discount.CategoryId,
                discount.ItemId,
                discount.DiscountTargetType,
                discount.OrderId,
                discount.InvoiceId,
                discount.Comments));

            return 0;
        }

        public async Task UpdateDiscountAsync(UpdateDiscountViewModel discount)
        {
            await _bus.SendCommandAsync(new UpdateDiscountCommand(
                discount.DiscountId,
                discount.DiscountTypeId,
                discount.CategoryId,
                discount.DiscountTargetType,
                discount.OrderId,
                discount.InvoiceId,
                discount.ItemId,
                discount.Comments));
        }

        public async Task DeleteDiscountAsync(int discountId)
        {
            await _bus.SendCommandAsync(new DeleteDiscountCommand(discountId));
        }
    }
}

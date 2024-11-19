using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.DiscountTypes;
using LibraRestaurant.Application.Queries.DiscountTypes.GetDiscountTypeById;
using LibraRestaurant.Application.Queries.DiscountTypes.GetAll;
using LibraRestaurant.Domain.Commands.DiscountTypes.CreateDiscountType;
using LibraRestaurant.Domain.Commands.DiscountTypes.UpdateDiscountType;
using LibraRestaurant.Domain.Commands.DiscountTypes.DeleteDiscountType;
using LibraRestaurant.Application.Queries.DiscountTypes.GetDiscountTypeByCode;

namespace LibraRestaurant.Application.Services
{
    public sealed class DiscountTypeService : IDiscountTypeService
    {
        private readonly IMediatorHandler _bus;

        public DiscountTypeService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<DiscountTypeViewModel?> GetDiscountTypeByIdAsync(int discountTypeId)
        {
            return await _bus.QueryAsync(new GetDiscountTypeByIdQuery(discountTypeId));
        }

        public async Task<DiscountTypeViewModel?> GetDiscountTypeByCodeAsync(string counponCode)
        {
            return await _bus.QueryAsync(new GetDiscountTypeByCodeQuery(counponCode));
        }

        public async Task<PagedResult<DiscountTypeViewModel>> GetAllDiscountTypesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllDiscountTypesQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreateDiscountTypeAsync(CreateDiscountTypeViewModel discountType)
        {
            await _bus.SendCommandAsync(new CreateDiscountTypeCommand(
                0,
                discountType.Name,
                discountType.Description,
                discountType.IsPercentage,
                discountType.Value,
                DateTime.Now,
                discountType.StartTime,
                discountType.EndTime,
                discountType.CounponCode,
                discountType.MinOrderValue,
                discountType.MinItemQuantity,
                discountType.MaxDiscountValue));

            return 0;
        }

        public async Task UpdateDiscountTypeAsync(UpdateDiscountTypeViewModel discountType)
        {
            await _bus.SendCommandAsync(new UpdateDiscountTypeCommand(
                discountType.DiscountTypeId,
                discountType.Name,
                discountType.Description,
                discountType.IsPercentage,
                discountType.Value,
                discountType.StartTime,
                discountType.EndTime,
                discountType.CounponCode,
                discountType.MinOrderValue,
                discountType.MinItemQuantity,
                discountType.MaxDiscountValue));
        }

        public async Task DeleteDiscountTypeAsync(int discountTypeId)
        {
            await _bus.SendCommandAsync(new DeleteDiscountTypeCommand(discountTypeId));
        }
    }
}

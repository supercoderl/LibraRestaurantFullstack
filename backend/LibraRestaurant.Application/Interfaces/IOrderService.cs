using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderViewModel?> GetOrderByIdAsync(Guid orderId);

        public Task<PagedResult<OrderViewModel>> GetAllOrdersAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            string? phone = null,
            SortQuery? sortQuery = null);

        public Task<PagedResult<OrderViewModel>> GetOrdersByPhoneAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            string? phone = null,
            SortQuery? sortQuery = null);

        public Task<Guid> CreateOrderAsync(CreateOrderViewModel order);
        public Task UpdateOrderAsync(UpdateOrderViewModel order);
        public Task DeleteOrderAsync(Guid orderId);
        public Task UpdatePaymentMethodAsync(Guid orderId, int paymentMethodId);
        public Task UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
    }
}

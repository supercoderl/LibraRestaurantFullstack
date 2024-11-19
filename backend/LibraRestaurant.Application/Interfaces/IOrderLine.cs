using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.OrderLines;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IOrderLineService
    {
        public Task<OrderLineViewModel?> GetOrderLineByIdAsync(int orderLineId);

        public Task<PagedResult<OrderLineViewModel>> GetAllOrderLinesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreateOrderLineAsync(CreateOrderLineViewModel orderLine);
        public Task<List<int>> CreateListOrderLineAsync(List<CreateOrderLineViewModel> orderLines);
        public Task UpdateOrderLineAsync(UpdateOrderLineViewModel orderLine);
        public Task DeleteOrderLineAsync(int orderLineId);
    }
}

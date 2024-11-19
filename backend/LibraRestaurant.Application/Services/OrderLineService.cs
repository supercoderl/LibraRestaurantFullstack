using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.OrderLines;
using LibraRestaurant.Application.Queries.OrderLines.GetOrderLineById;
using LibraRestaurant.Application.Queries.OrderLines.GetAll;
using LibraRestaurant.Domain.Commands.OrderLines.CreateOrderLine;
using LibraRestaurant.Domain.Commands.OrderLines.UpdateOrderLine;
using LibraRestaurant.Domain.Commands.OrderLines.DeleteOrderLine;
using LibraRestaurant.Application.Queries.OrderLines.GetOrderLineByOrderAndItem;

namespace LibraRestaurant.Application.Services
{
    public sealed class OrderLineService : IOrderLineService
    {
        private readonly IMediatorHandler _bus;

        public OrderLineService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<OrderLineViewModel?> GetOrderLineByIdAsync(int orderLineId)
        {
            return await _bus.QueryAsync(new GetOrderLineByIdQuery(orderLineId));
        }

        public async Task<PagedResult<OrderLineViewModel>> GetAllOrderLinesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllOrderLinesQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreateOrderLineAsync(CreateOrderLineViewModel orderLine)
        {
            await _bus.SendCommandAsync(new CreateOrderLineCommand(
                0,
                orderLine.OrderId,
                orderLine.ItemId,
                orderLine.Quantity,
                orderLine.FoodPrice,
                orderLine.IsCanceled,
                orderLine.CanceledTime,
                orderLine.CanceledReason,
                orderLine.CustomerReview,
                orderLine.CustomerLike));

            return 0;
        }

        public async Task UpdateOrderLineAsync(UpdateOrderLineViewModel orderLine)
        {
            await _bus.SendCommandAsync(new UpdateOrderLineCommand(
                orderLine.OrderLineId,
                orderLine.OrderId,
                orderLine.ItemId,
                orderLine.Quantity,
                orderLine.FoodPrice,
                orderLine.IsCanceled,
                orderLine.CanceledTime,
                orderLine.CanceledReason,
                orderLine.CustomerReview,
                orderLine.CustomerLike));
        }

        public async Task DeleteOrderLineAsync(int orderLineId)
        {
            await _bus.SendCommandAsync(new DeleteOrderLineCommand(orderLineId));
        }

        public async Task<List<int>> CreateListOrderLineAsync(List<CreateOrderLineViewModel> orderLines)
        {
            List<int> result = new List<int>();
            List<CreateOrderLineViewModel> newList = await FilterItemToCreate(orderLines);

            foreach (var orderLine in newList)
            {
                await _bus.SendCommandAsync(new CreateOrderLineCommand(
                0,
                orderLine.OrderId,
                orderLine.ItemId,
                orderLine.Quantity,
                orderLine.FoodPrice,
                orderLine.IsCanceled,
                orderLine.CanceledTime,
                orderLine.CanceledReason,
                orderLine.CustomerReview,
                orderLine.CustomerLike));

                result.Add(0);
            }
            return result;
        }

        private async Task<List<CreateOrderLineViewModel>> FilterItemToCreate(List<CreateOrderLineViewModel> orderLines)
        {
            List<CreateOrderLineViewModel> newList = new List<CreateOrderLineViewModel>();
            foreach(var item in orderLines)
            {
                var orderLine = await _bus.QueryAsync(new GetOrderLineByOrderAndItemQuery(item.OrderId, item.ItemId));
                if(orderLine != null)
                {
                    //If exists, update it
                    await _bus.SendCommandAsync(new UpdateOrderLineCommand(
                        orderLine.OrderLineId,
                        orderLine.OrderId,
                        orderLine.ItemId,
                        item.Quantity,
                        item.FoodPrice,
                        orderLine.IsCanceled,
                        orderLine.CanceledTime,
                        orderLine.CanceledReason,
                        orderLine.CustomerReview,
                        orderLine.CustomerLike
                    ));
                }
                else
                {
                    //If not exists, add to list to create
                    newList.Add(item);
                }
            }
            return newList;
        }
    }
}

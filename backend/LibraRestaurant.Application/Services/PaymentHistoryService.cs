using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentHistoryById;
using LibraRestaurant.Application.Queries.PaymentHistories.GetAll;
using LibraRestaurant.Application.ViewModels.PaymentHistorys;
using LibraRestaurant.Domain.Commands.PaymentHistories.CreatePaymentHistory;
using LibraRestaurant.Domain.Commands.PaymentHistories.DeletePaymentHistory;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentHistoryByOrder;

namespace LibraRestaurant.Application.Services
{
    public sealed class PaymentHistoryService : IPaymentHistoryService
    {
        private readonly IMediatorHandler _bus;
        private readonly IOrderService _orderService;

        public PaymentHistoryService(IMediatorHandler bus, IOrderService orderService)
        {
            _bus = bus;
            _orderService = orderService;
        }

        public async Task<PaymentHistoryViewModel?> GetPaymentHistoryByIdAsync(int paymentHistoryId)
        {
            return await _bus.QueryAsync(new GetPaymentHistoryByIdQuery(paymentHistoryId));
        }

        public async Task<PagedResult<PaymentHistoryViewModel>> GetAllPaymentHistoriesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllPaymentHistoriesQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreatePaymentHistoryAsync(CreatePaymentHistoryViewModel paymentHistory)
        {
            if(paymentHistory.Status == PaymentStatus.Success)
            {
                await _orderService.UpdateOrderStatusAsync(paymentHistory.OrderId, OrderStatus.Paid);
            }

            var paymentExist = await _bus.QueryAsync(new GetPaymentHistoryByOrderQuery(paymentHistory.OrderId));

            if (paymentExist is not null) return 0;

            await _bus.SendCommandAsync(new CreatePaymentHistoryCommand(
                0,
                paymentHistory.TransactionId,
                paymentHistory.OrderId,
                paymentHistory.PaymentMethodId,
                paymentHistory.Amount,
                paymentHistory.CurrencyId,
                paymentHistory.ResponseJSON,
                paymentHistory.CallbackURL,
                DateTime.Now,
                paymentHistory.Status));

            return 0;
        }

        public async Task DeletePaymentHistoryAsync(int paymentHistoryId)
        {
            await _bus.SendCommandAsync(new DeletePaymentHistoryCommand(paymentHistoryId));
        }
    }
}

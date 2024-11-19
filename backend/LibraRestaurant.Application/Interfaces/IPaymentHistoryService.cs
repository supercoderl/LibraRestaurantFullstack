using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using LibraRestaurant.Application.ViewModels.PaymentHistorys;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IPaymentHistoryService
    {
        public Task<PaymentHistoryViewModel?> GetPaymentHistoryByIdAsync(int paymentHistoryId);

        public Task<PagedResult<PaymentHistoryViewModel>> GetAllPaymentHistoriesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreatePaymentHistoryAsync(CreatePaymentHistoryViewModel paymentHistory);
        public Task DeletePaymentHistoryAsync(int paymentHistoryId);
    }
}

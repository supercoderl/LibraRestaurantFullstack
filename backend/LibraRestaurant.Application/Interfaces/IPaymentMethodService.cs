using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.PaymentMethods;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IPaymentMethodService
    {
        public Task<PaymentMethodViewModel?> GetPaymentMethodByIdAsync(int paymentMethodId);

        public Task<PagedResult<PaymentMethodViewModel>> GetAllPaymentMethodsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreatePaymentMethodAsync(CreatePaymentMethodViewModel paymentMethod);
        public Task UpdatePaymentMethodAsync(UpdatePaymentMethodViewModel paymentMethod);
        public Task DeletePaymentMethodAsync(int paymentMethodId);
    }
}

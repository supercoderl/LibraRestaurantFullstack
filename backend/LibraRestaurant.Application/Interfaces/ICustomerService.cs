using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Customers;

namespace LibraRestaurant.Application.Interfaces
{
    public interface ICustomerService
    {
        public Task<CustomerViewModel?> GetCustomerByIdAsync(int customerId);

        public Task<PagedResult<CustomerViewModel>> GetAllCustomersAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<int> CreateCustomerAsync(CreateCustomerViewModel customer);
        public Task UpdateCustomerAsync(UpdateCustomerViewModel customer);
        public Task DeleteCustomerAsync(int customerId);
    }
}

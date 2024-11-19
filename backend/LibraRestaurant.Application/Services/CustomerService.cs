using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Customers;
using LibraRestaurant.Application.Queries.Customers.GetCustomerById;
using LibraRestaurant.Application.Queries.Customers.GetAll;
using LibraRestaurant.Domain.Commands.Customers.CreateCustomer;
using LibraRestaurant.Domain.Commands.Customers.UpdateCustomer;
using LibraRestaurant.Domain.Commands.Customers.DeleteCustomer;

namespace LibraRestaurant.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediatorHandler _bus;

        public CustomerService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<CustomerViewModel?> GetCustomerByIdAsync(int customerId)
        {
            return await _bus.QueryAsync(new GetCustomerByIdQuery(customerId));
        }

        public async Task<PagedResult<CustomerViewModel>> GetAllCustomersAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllCustomersQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<int> CreateCustomerAsync(CreateCustomerViewModel customer)
        {
            await _bus.SendCommandAsync(new CreateCustomerCommand(
                0,
                customer.Name,
                customer.Phone,
                customer.Email,
                customer.Address));

            return 0;
        }

        public async Task UpdateCustomerAsync(UpdateCustomerViewModel customer)
        {
            await _bus.SendCommandAsync(new UpdateCustomerCommand(
                customer.CustomerId,
                customer.Name,
                customer.Phone,
                customer.Email,
                customer.Address));
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _bus.SendCommandAsync(new DeleteCustomerCommand(customerId));
        }
    }
}

using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Customers;
using LibraRestaurant.Shared.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public class CustomersContext : ICustomersContext
    {
        private readonly CustomersApi.CustomersApiClient _client;

        public CustomersContext(CustomersApi.CustomersApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomersByIds(IEnumerable<int> ids)
        {
            var request = new GetCustomersByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Customers.Select(customer => new CustomerViewModel(
                customer.CustomerId,
                customer.Name,
                customer.Phone,
                customer.Email,
                customer.Address));
        }
    }
}

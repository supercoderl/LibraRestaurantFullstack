using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Currencies;
using LibraRestaurant.Proto.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class CustomersApiImplementation : CustomersApi.CustomersApiBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersApiImplementation(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public override async Task<GetCustomersByIdsResult> GetByIds(
            GetCustomersByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var customers = await _customerRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(customer => idsAsIntegers.Contains(customer.CustomerId))
                .Select(customer => new GrpcCustomer
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    Address = customer.Address,
                })
                .ToListAsync();

            var result = new GetCustomersByIdsResult();

            result.Customers.AddRange(customers);

            return result;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Queries.Currencies.GetCurrencyById;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Customers;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Customers.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler :
    IRequestHandler<GetCustomerByIdQuery, CustomerViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMediatorHandler bus)
    {
        _customerRepository = customerRepository;
        _bus = bus;
    }

    public async Task<CustomerViewModel?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCurrencyByIdQuery),
                    $"Customer with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return CustomerViewModel.FromCustomer(customer);
    }
}
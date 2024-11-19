
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Currency;
using System;
using LibraRestaurant.Shared.Events.Customer;

namespace LibraRestaurant.Domain.Commands.Customers.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICustomerRepository customerRepository) : base(bus, unitOfWork, notifications)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return -1;
            } 

            var customer = new Entities.Customer(
                request.CustomerId,
                request.Name,
                request.Phone,
                request.Email,
                request.Address,
                DateTime.Now,
                null);

            _customerRepository.Add(customer);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CustomerCreatedEvent(customer.CustomerId));
            }

            return customer.CustomerId;
        }
    }
}

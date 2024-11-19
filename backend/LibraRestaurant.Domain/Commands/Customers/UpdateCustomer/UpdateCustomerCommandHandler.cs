
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.MenuItem;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Currency;
using LibraRestaurant.Shared.Events.Customer;

namespace LibraRestaurant.Domain.Commands.Customers.UpdateCustomer
{
    public sealed class UpdateCustomerCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICustomerRepository customerRepository) : base(bus, unitOfWork, notifications)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no customer with Id {request.CustomerId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            customer.SetName(request.Name);
            customer.SetPhone(request.Phone);
            customer.SetEmail(request.Email);
            customer.SetAddress(request.Address);
            customer.SetUpdatedAt(DateTime.Now);

            _customerRepository.Update(customer);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CustomerUpdatedEvent(customer.CustomerId));
            }
        }
    }
}

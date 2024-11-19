
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
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Currency;
using LibraRestaurant.Shared.Events.Customer;

namespace LibraRestaurant.Domain.Commands.Customers.DeleteCustomer
{
    public sealed class DeleteCustomerCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICustomerRepository customerRepository) : base(bus, unitOfWork, notifications)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
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

            _customerRepository.Remove(customer);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CustomerDeletedEvent(request.CustomerId));
            }
        }
    }
}

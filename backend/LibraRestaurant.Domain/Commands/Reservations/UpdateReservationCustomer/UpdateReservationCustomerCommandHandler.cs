
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
using LibraRestaurant.Domain.Commands.Reservations.UpdateReservation;
using LibraRestaurant.Shared.Events.Reservation;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using LibraRestaurant.Domain.Commands.Customers.CreateCustomer;

namespace LibraRestaurant.Domain.Commands.Reservations.UpdateReservationCustomer
{
    public sealed class UpdateReservationCustomerCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateReservationCustomerCommand, int>
    {
        private readonly IReservationRepository _reservationRepository;

        public UpdateReservationCustomerCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReservationRepository reservationRepository) : base(bus, unitOfWork, notifications)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<int> Handle(UpdateReservationCustomerCommand request, CancellationToken cancellationToken)
        {
            int customerId = -1;

            if (!await TestValidityAsync(request))
            {
                return customerId;
            }

            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);

            if (reservation is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no reservation with Id {request.ReservationId}",
                        ErrorCodes.ObjectNotFound));
                return customerId;
            }

            reservation.SetStatus(request.Status);
   
            if(string.IsNullOrEmpty(request.CustomerName) || string.IsNullOrEmpty(request.CustomerPhone))
            {
                reservation.SetCustomer(null);
            }
            else
            {
                customerId = await Bus.QueryAsync(new CreateCustomerCommand(0, request.CustomerName, request.CustomerPhone, null, null));

                if (customerId == -1)
                {
                    await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"An error occur when saving customer's data.",
                        ErrorCodes.CommitFailed));
                    return -1;
                }

                reservation.SetCustomer(customerId);
            }

            _reservationRepository.Update(reservation);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReservationUpdatedEvent(reservation.ReservationId));
            }

            return customerId;
        }
    }
}


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

namespace LibraRestaurant.Domain.Commands.Reservation.UpdateReservation
{
    public sealed class UpdateReservationCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public UpdateReservationCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReservationRepository reservationRepository) : base(bus, unitOfWork, notifications)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);

            if (reservation is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no reservation with Id {request.ReservationId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            reservation.SetTableNumber(request.TableNumber);
            reservation.SetCapacity(request.Capacity);
            reservation.SetStoreId(request.StoreId);
            reservation.SetStatus(request.Status);
            reservation.SetDescription(request.Description);
            reservation.SetReservationTime(request.ReservationTime);
            reservation.SetCustomer(request.CustomerId);
            reservation.SetCode(request.Code);
            reservation.SetCleaningTime(request.CleaningTime);

            _reservationRepository.Update(reservation);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReservationUpdatedEvent(reservation.ReservationId));
            }
        }
    }
}

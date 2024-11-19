
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
using LibraRestaurant.Shared.Events.Reservation;

namespace LibraRestaurant.Domain.Commands.Reservations.DeleteReservation
{
    public sealed class DeleteReservationCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReservationRepository reservationRepository) : base(bus, unitOfWork, notifications)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
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

            _reservationRepository.Remove(reservation);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReservationDeletedEvent(request.ReservationId));
            }
        }
    }
}

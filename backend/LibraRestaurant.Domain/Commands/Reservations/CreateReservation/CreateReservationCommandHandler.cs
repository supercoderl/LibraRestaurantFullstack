
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.Reservation;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System;
using LibraRestaurant.Shared.Reservations;

namespace LibraRestaurant.Domain.Commands.Reservations.CreateReservation
{
    public sealed class CreateReservationCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateReservationCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public CreateReservationCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReservationRepository reservationRepository) : base(bus, unitOfWork, notifications)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var reservation = new Entities.Reservation(
                request.ReservationId,
                request.TableNumber,
                request.Capacity,
                request.Status,
                request.StoreId,
                request.Description,
                request.ReservationTime,
                request.CustomerId,
                request.Code,
                request.CleaningTime);

            _reservationRepository.Add(reservation);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReservationCreatedEvent(reservation.ReservationId));
            }
        }
    }
}

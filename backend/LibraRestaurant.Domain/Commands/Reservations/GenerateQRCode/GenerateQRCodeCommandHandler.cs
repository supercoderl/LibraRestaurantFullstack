
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
using LibraRestaurant.Domain.Errors;

namespace LibraRestaurant.Domain.Commands.Reservations.GenerateQRCode
{
    public sealed class GenerateQRCodeCommandHandler : CommandHandlerBase,
        IRequestHandler<GenerateQRCodeCommand>
    {
        private readonly IReservationRepository _reservationRepository;

        public GenerateQRCodeCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReservationRepository reservationRepository) : base(bus, unitOfWork, notifications)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(GenerateQRCodeCommand request, CancellationToken cancellationToken)
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

            if(reservation.Code is not null)
            {
                return;
            }

            string qrData = string.Concat(
                        "{",
                            "\"reservationId\":\"", reservation.ReservationId, "\",",
                            "\"tableNumber\":\"", reservation.TableNumber, "\",",
                            "\"capacity\":\"", reservation.Capacity, "\",",
                            "\"storeId\":\"", reservation.StoreId, "\",",
                            "\"description\":\"", reservation.Description, "\"",
                        "}"
                    );

            reservation.SetCode(await GenerateQRCode(qrData));

            _reservationRepository.Update(reservation);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReservationUpdatedEvent(reservation.ReservationId));
            }
        }

        public async Task HandleUpdate(Domain.Entities.Reservation request)
        {
            _reservationRepository.Update(request);
            await CommitAsync();
        }

        //Generate QR Code
        private async Task<string> GenerateQRCode(string qrData)
        {
            await Task.CompletedTask;
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            Bitmap bitmap = code.GetGraphic(60);
            byte[] bitmapArray = BitmapToByteArray(bitmap);
            string qrURL = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bitmapArray));
            return qrURL;
        }

        //Convert bit map to byte array
        private byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}

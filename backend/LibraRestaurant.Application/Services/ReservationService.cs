using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Application.Queries.Reservations.GetReservationById;
using LibraRestaurant.Application.Queries.Reservations.GetAll;
using LibraRestaurant.Domain.Commands.Reservations.CreateReservation;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Commands.Reservations.UpdateReservation;
using LibraRestaurant.Domain.Commands.Reservations.DeleteReservation;
using System.IO;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using LibraRestaurant.Application.Queries.Reservations.GetReservationByTableNumberAndStoreId;
using LibraRestaurant.Application.Queries.Reservations.GetAllTablesRealTime;
using System.Collections.Generic;
using LibraRestaurant.Domain.Commands.Reservations.UpdateReservationCustomer;
using LibraRestaurant.Application.Queries.Reservations.GetReservationByStatus;

namespace LibraRestaurant.Application.Services
{
    public sealed class ReservationService : IReservationService
    {
        private readonly IMediatorHandler _bus;
        private readonly IImageService _imageService;

        public ReservationService(IMediatorHandler bus, IImageService imageService)
        {
            _bus = bus;
            _imageService = imageService;
        }

        public async Task<ReservationViewModel?> GetReservationByIdAsync(int reservationId)
        {
            return await _bus.QueryAsync(new GetReservationByIdQuery(reservationId));
        }

        public async Task<PagedResult<ReservationViewModel>> GetAllReservationsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllReservationsQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<List<TableRealTimeViewModel>> GetAllTablesRealTimeAsync(bool includeDeleted)
        {
            return await _bus.QueryAsync(new GetAllTablesRealTimeQuery(includeDeleted));
        }

        public async Task<ReservationViewModel?> GetReservationByTableNumberAndStoreIdAsync(int tableNumber, Guid storeId)
        {
            return await _bus.QueryAsync(new GetReservationByTableNumberAndStoreIdQuery(tableNumber, storeId));
        }

        public async Task<int?> GetReservationStatus(int reservationId)
        {
            var reservation = await _bus.QueryAsync(new GetReservationByIdQuery(reservationId));
            if (reservation is not null)
            {
                return Convert.ToInt32(reservation.Status);
            }
            return null;
        }

        public async Task<int> CreateReservationAsync(CreateReservationViewModel reservation)
        {
            string qrData = string.Concat(
                        "{",
                            "\"tableNumber\":\"", reservation.TableNumber, "\",",
                            "\"capacity\":\"", reservation.Capacity, "\",",
                            "\"storeId\":\"", reservation.StoreId, "\",",
                            "\"description\":\"", reservation.Description, "\"",
                        "}"
                    );

            var qr = await _imageService.UploadFile(await GenerateQRCodeAsync(qrData), string.Concat(reservation.TableNumber.ToString(), "-", reservation.StoreId), "QRs");

            await _bus.SendCommandAsync(new CreateReservationCommand(
                0,
                reservation.TableNumber,
                reservation.Capacity,
                ReservationStatus.Available,
                reservation.StoreId,
                reservation.Description,
                reservation.ReservationTime,
                reservation.CustomerId,
                qr,
                reservation.CleaningTime));

            return 0;
        }

        public async Task UpdateReservationAsync(UpdateReservationViewModel reservation)
        {
            string QRCode = string.Empty;

            if(string.IsNullOrEmpty(reservation.Code))
            {
                string qrData = string.Concat(
                        "{",
                            "\"tableNumber\":\"", reservation.TableNumber, "\",",
                            "\"capacity\":\"", reservation.Capacity, "\",",
                            "\"storeId\":\"", reservation.StoreId, "\",",
                            "\"description\":\"", reservation.Description, "\"",
                        "}"
                    );

                QRCode = await _imageService.UploadFile(await GenerateQRCodeAsync(qrData), string.Concat(reservation.TableNumber.ToString(), "-", reservation.StoreId), "QRs");
            }

            else
            {
                QRCode = reservation.Code;
            }

            await _bus.SendCommandAsync(new UpdateReservationCommand(
                reservation.ReservationId,
                reservation.TableNumber,
                reservation.Capacity,
                reservation.Status,
                reservation.StoreId,
                reservation.Description,
                reservation.ReservationTime,
                reservation.CustomerId,
                QRCode,
                reservation.CleaningTime));
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            await _bus.SendCommandAsync(new DeleteReservationCommand(reservationId));
        }

        public async Task<int> UpdateReservationCustomerAsync(UpdateReservationCustomerViewModel reservationCustomer)
        {
            return await _bus.QueryAsync(new UpdateReservationCustomerCommand(
                reservationCustomer.ReservationId,
                reservationCustomer.Status,
                reservationCustomer.CustomerName,
                reservationCustomer.CustomerPhone));
        }

        //Generate QR Code
        private async Task<string> GenerateQRCodeAsync(string qrData)
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

        public async Task<List<ReservationViewModel>> GetAllReservationsByStatusAsync(ReservationStatus status)
        {
            return await _bus.QueryAsync(new GetReservationByStatusQuery(status));
        }
    }
}

using Grpc.Core;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Proto.Menus;
using LibraRestaurant.Proto.Reservations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.gRPC
{
    public sealed class ReservationsApiImplementation : ReservationsApi.ReservationsApiBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationsApiImplementation(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public override async Task<GetReservationsByIdsResult> GetByIds(
            GetReservationsByIdsRequest request,
            ServerCallContext context)
        {
            var idsAsIntegers = new List<int>(request.Ids.Count);

            foreach (var id in request.Ids)
            {
                idsAsIntegers.Add(id);
            }

            var reservations = await _reservationRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(reservation => idsAsIntegers.Contains(reservation.ReservationId))
                .Select(reservation => new GrpcReservation
                {
                    ReservationId = reservation.ReservationId,
                    TableNumber = reservation.TableNumber,
                    Capacity = reservation.Capacity,
                    StoreId = reservation.StoreId.ToString(),
                    Description = reservation.Description,
                    ReservationTime = reservation.ReservationTime.ToString(),
                    CustomerId = reservation.CustomerId ?? 0,
                    Code = reservation.Code,
                    CleaningTime = reservation.CleaningTime.ToString(),
                    IsDeleted = reservation.Deleted
                })
                .ToListAsync();

            var result = new GetReservationsByIdsResult();

            result.Reservations.AddRange(reservations);

            return result;
        }
    }
}

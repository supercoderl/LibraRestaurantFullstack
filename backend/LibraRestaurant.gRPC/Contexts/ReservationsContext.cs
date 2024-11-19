using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Reservations;
using LibraRestaurant.Shared.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Contexts
{
    public sealed class ReservationsContext : IReservationsContext
    {
        private readonly ReservationsApi.ReservationsApiClient _client;

        public ReservationsContext(ReservationsApi.ReservationsApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ReservationViewModel>> GetReservationsByIds(IEnumerable<int> ids)
        {
            var request = new GetReservationsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            var result = await _client.GetByIdsAsync(request);

            return result.Reservations.Select(reservation => new ReservationViewModel(
                reservation.ReservationId,
                reservation.TableNumber,
                reservation.Capacity,
                Guid.Parse(reservation.StoreId),
                reservation.Description,
                DateTime.Parse(reservation.ReservationTime),
                reservation.CustomerId,
                reservation.Code,
                DateTime.Parse(reservation.CleaningTime),
                reservation.IsDeleted));
        }
    }
}

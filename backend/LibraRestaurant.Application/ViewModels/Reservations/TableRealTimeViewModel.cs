using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Application.ViewModels.Reservations
{
    public sealed class TableRealTimeViewModel
    {
        public string? TableKey { get; set; }

        public static TableRealTimeViewModel FromTables(Reservation reservation)
        {
            return new TableRealTimeViewModel
            {
                TableKey = string.Concat(reservation.StoreId, "-", reservation.TableNumber)
            };
        }
    }
}

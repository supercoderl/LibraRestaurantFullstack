using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.SortProviders
{
    public sealed class ReservationViewModelSortProvider : ISortingExpressionProvider<ReservationViewModel, Reservation>
    {
        private static readonly Dictionary<string, Expression<Func<Reservation, object>>> s_expressions = new()
    {
        { "tableNumber", reservation => reservation.TableNumber },
        { "capacity", reservation => reservation.Capacity },
        { "status", reservation => reservation.Status },
        { "storeId", reservation => reservation.StoreId },
        { "description", reservation => reservation.Description ?? string.Empty },
        { "reservationTime", reservation => reservation.ReservationTime ?? DateTime.Now },
    };

        public Dictionary<string, Expression<Func<Reservation, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}

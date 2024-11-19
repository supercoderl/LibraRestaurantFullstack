using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IReservationService
    {
        public Task<ReservationViewModel?> GetReservationByIdAsync(int reservationId);

        public Task<int?> GetReservationStatus(int reservationId);

        public Task<ReservationViewModel?> GetReservationByTableNumberAndStoreIdAsync(int tableNumber, Guid storeId);

        public Task<PagedResult<ReservationViewModel>> GetAllReservationsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<List<TableRealTimeViewModel>> GetAllTablesRealTimeAsync(bool includeDeleted);
        public Task<int> CreateReservationAsync(CreateReservationViewModel reservation);
        public Task UpdateReservationAsync(UpdateReservationViewModel reservation);
        public Task DeleteReservationAsync(int reservationId);
        public Task<int> UpdateReservationCustomerAsync(UpdateReservationCustomerViewModel reservationCustomer);
        public Task<List<ReservationViewModel>> GetAllReservationsByStatusAsync(ReservationStatus status);
    }
}

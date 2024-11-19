using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Reviews;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IReviewService
    {
        public Task<ReviewViewModel?> GetReviewByIdAsync(int reviewId);

        public Task<PagedResult<ReviewViewModel>> GetAllReviewsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            int? itemId = null,
            SortQuery? sortQuery = null);

        public Task<int> CreateReviewAsync(CreateReviewViewModel review);
        public Task UpdateReviewAsync(UpdateReviewViewModel review);
        public Task DeleteReviewAsync(int reservationId);
    }
}

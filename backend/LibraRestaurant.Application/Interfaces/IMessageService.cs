using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Messages;

namespace LibraRestaurant.Application.Interfaces
{
    public interface IMessageService
    {

        public Task<PagedResult<MessageViewModel>> GetAllMessagesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            string? type = null,
            SortQuery? sortQuery = null);

        public Task<int> SendMessageAsync(SendMessageViewModel message);
        public Task UpdateMessageAsync(UpdateMessageViewModel message);
    }
}

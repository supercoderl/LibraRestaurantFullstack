using LibraRestaurant.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Hubs
{
    public class TrackerHub : Hub
    {
        private readonly IMessageService _messageService;

        public TrackerHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Join Group Without Authorization
        /// </summary>
        /// <param name="tableNames"></param>
        public async Task JoinAllTableGroups(List<string> tableNames)
        {
            foreach (var tableName in tableNames)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tableName);
            }
        }

        /// <summary>
        /// Join Group Without Authorization
        /// </summary>
        /// <param name="tableName"></param>
        public async Task JoinTableGroup(string tableName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, tableName);
        }

        /// <summary>
        /// Notify Event to Clients With Group
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tableName"></param>
        public async Task NotifyOrderPlaced(string tableName, string message, string type)
        {
            // Gửi thông báo đến tất cả thành viên trong nhóm bàn
            await Clients.Group(tableName).SendAsync(nameof(NotifyEvent), message);
            await _messageService.SendMessageAsync(new ViewModels.Messages.SendMessageViewModel(
                null,
                tableName,
                message,
                tableName,
                type,
                null
            ));
        }

        /// <summary>
        /// Notify Event to Clients Without Group
        /// </summary>
        /// <param name="message"></param>
        public async Task NotifyEvent(string message)
        {
            await Clients.All.SendAsync(nameof(NotifyEvent), message);
        }

        public enum HubMethodType
        {
            NotifyEvent
        }
    }
}

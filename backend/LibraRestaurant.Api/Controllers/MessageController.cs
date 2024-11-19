using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Messages;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Application.Services;
using LibraRestaurant.Application.ViewModels.Menus;
using Microsoft.AspNetCore.Authorization;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class MessageController : ApiController
    {
        private readonly IMessageService _messageService;

        public MessageController(
            INotificationHandler<DomainNotification> notifications,
            IMessageService messageService) : base(notifications)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all messages")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<MessageViewModel>>))]
        public async Task<IActionResult> GetAllMessagesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] string? type = null,
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<MessageViewModelSortProvider, MessageViewModel, Message>]
        SortQuery? sortQuery = null)
        {
            var messages = await _messageService.GetAllMessagesAsync(
                query,
                includeDeleted,
                searchTerm,
                type,
                sortQuery);
            return Response(messages);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("Update a message")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateMessageViewModel>))]
        public async Task<IActionResult> UpdateMessageAsync([FromBody] UpdateMessageViewModel viewModel)
        {
            await _messageService.UpdateMessageAsync(viewModel);
            return Response(viewModel);
        }
    }
}

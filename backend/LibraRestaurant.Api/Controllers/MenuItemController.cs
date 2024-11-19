using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.MenuItems;
using LibraRestaurant.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class ItemController : ApiController
    {
        private readonly IMenuItemService _itemService;

        public ItemController(
            INotificationHandler<DomainNotification> notifications,
            IMenuItemService itemService) : base(notifications)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all items")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ItemViewModel>>))]
        public async Task<IActionResult> GetAllItemsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<ItemViewModelSortProvider, ItemViewModel, MenuItem>]
        SortQuery? sortQuery = null,
            [FromQuery] int categoryId = -1)
        {
            var items = await _itemService.GetAllItemsAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery,
                categoryId);
            return Response(items);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a item by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ItemViewModel>))]
        public async Task<IActionResult> GetItemByIdAsync([FromRoute] int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            return Response(item);
        }

        [HttpGet("slug/{slug}")]
        [SwaggerOperation("Get a item by slug")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ItemViewModel>))]
        public async Task<IActionResult> GetItemBySlugAsync([FromRoute] string slug)
        {
            var item = await _itemService.GetItemBySlugAsync(slug);
            return Response(item);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("Create a new item")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemViewModel viewModel)
        {
            var itemId = await _itemService.CreateItemAsync(viewModel);
            return Response(itemId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a item")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteItemAsync([FromRoute] int id)
        {
            await _itemService.DeleteItemAsync(id);
            return Response(id);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("Update a item")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateItemViewModel>))]
        public async Task<IActionResult> UpdateItemAsync([FromBody] UpdateItemViewModel viewModel)
        {
            try
            {
                await _itemService.UpdateItemAsync(viewModel);
                return Response(viewModel);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}

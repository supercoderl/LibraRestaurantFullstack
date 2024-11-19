using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Stores;
using LibraRestaurant.Domain.Entities;
using System;
using Microsoft.AspNetCore.Authorization;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class StoreController : ApiController
    {
        private readonly IStoreService _storeService;

        public StoreController(
            INotificationHandler<DomainNotification> notifications,
            IStoreService storeService) : base(notifications)
        {
            _storeService = storeService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all stores")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<StoreViewModel>>))]
        public async Task<IActionResult> GetAllStoresAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<StoreViewModelSortProvider, StoreViewModel, Store>]
        SortQuery? sortQuery = null)
        {
            var stores = await _storeService.GetAllStoresAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(stores);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a store by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<StoreViewModel>))]
        public async Task<IActionResult> GetStoreByIdAsync([FromRoute] Guid id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            return Response(store);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("Create a new store")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateMenuAsync([FromBody] CreateStoreViewModel viewModel)
        {
            var storeId = await _storeService.CreateStoreAsync(viewModel);
            return Response(storeId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a store")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> DeleteStoreAsync([FromRoute] Guid id)
        {
            await _storeService.DeleteStoreAsync(id);
            return Response(id);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("Update a store")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateStoreViewModel>))]
        public async Task<IActionResult> UpdateStoreAsync([FromBody] UpdateStoreViewModel viewModel)
        {
            await _storeService.UpdateStoreAsync(viewModel);
            return Response(viewModel);
        }
    }
}

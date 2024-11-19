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
using LibraRestaurant.Application.ViewModels.OrderLines;
using LibraRestaurant.Domain.Entities;
using System.Collections.Generic;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    /*    [Authorize]*/
    [Route("/api/v1/[controller]")]
    public sealed class OrderLineController : ApiController
    {
        private readonly IOrderLineService _orderLineService;

        public OrderLineController(
            INotificationHandler<DomainNotification> notifications,
            IOrderLineService orderLineService) : base(notifications)
        {
            _orderLineService = orderLineService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all order lines")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<OrderLineViewModel>>))]
        public async Task<IActionResult> GetAllOrderLinesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<OrderLineViewModelSortProvider, OrderLineViewModel, OrderLine>]
        SortQuery? sortQuery = null)
        {
            var orderLines = await _orderLineService.GetAllOrderLinesAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(orderLines);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a order line by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<OrderLineViewModel>))]
        public async Task<IActionResult> GetOrderLineByIdAsync([FromRoute] int id)
        {
            var orderLine = await _orderLineService.GetOrderLineByIdAsync(id);
            return Response(orderLine);
        }

        [HttpPost]
        [SwaggerOperation("Create a new orderLine")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateOrderLineAsync([FromBody] CreateOrderLineViewModel viewModel)
        {
            var orderLineId = await _orderLineService.CreateOrderLineAsync(viewModel);
            return Response(orderLineId);
        }

        [HttpPost("list")]
        [SwaggerOperation("Create a list orderLine")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateListOrderLineAsync([FromBody] List<CreateOrderLineViewModel> viewModels)
        {
            var orderLineIds = await _orderLineService.CreateListOrderLineAsync(viewModels);
            return Response(orderLineIds);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a order line")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteOrderLineAsync([FromRoute] int id)
        {
            await _orderLineService.DeleteOrderLineAsync(id);
            return Response(id);
        }

        [HttpPut]
        [SwaggerOperation("Update a order line")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateOrderLineViewModel>))]
        public async Task<IActionResult> UpdateOrderLineAsync([FromBody] UpdateOrderLineViewModel viewModel)
        {
            await _orderLineService.UpdateOrderLineAsync(viewModel);
            return Response(viewModel);
        }
    }
}

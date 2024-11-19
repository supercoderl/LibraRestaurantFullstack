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
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Domain.Entities;
using System;
using Microsoft.AspNetCore.Authorization;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(
            INotificationHandler<DomainNotification> notifications,
            IOrderService orderService) : base(notifications)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("Get a list of all orders")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<OrderViewModel>>))]
        public async Task<IActionResult> GetAllOrdersAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] string? phone = null,
            [FromQuery] [SortableFieldsAttribute<OrderViewModelSortProvider, OrderViewModel, OrderHeader>]
        SortQuery? sortQuery = null)
        {
            var orders = await _orderService.GetAllOrdersAsync(
                query,
                includeDeleted,
                searchTerm,
                phone,
                sortQuery);
            return Response(orders);
        }

        [HttpGet("customer")]
        [SwaggerOperation("Get a list of all orders")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<OrderViewModel>>))]
        public async Task<IActionResult> GetOrdersByPhoneAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] string? phone = null,
            [FromQuery] [SortableFieldsAttribute<OrderViewModelSortProvider, OrderViewModel, OrderHeader>]
        SortQuery? sortQuery = null)
        {
            var orders = await _orderService.GetOrdersByPhoneAsync(
                query,
                includeDeleted,
                searchTerm,
                phone,
                sortQuery);
            return Response(orders);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a order by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<OrderViewModel>))]
        public async Task<IActionResult> GetOrderByIdAsync([FromRoute] Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Response(order);
        }

        [HttpPost]
        [SwaggerOperation("Create a new order")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderViewModel viewModel)
        {
            var orderId = await _orderService.CreateOrderAsync(viewModel);
            return Response(orderId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a order")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            return Response(id);
        }

        [HttpPut]
        [SwaggerOperation("Update a order")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateOrderViewModel>))]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] UpdateOrderViewModel viewModel)
        {
            await _orderService.UpdateOrderAsync(viewModel);
            return Response(viewModel);
        }
    }
}

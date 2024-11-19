using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class ReservationController : ApiController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(
            INotificationHandler<DomainNotification> notifications,
            IReservationService reservationService) : base(notifications)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all reservations")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ReservationViewModel>>))]
        public async Task<IActionResult> GetAllReservationsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<ReservationViewModelSortProvider, ReservationViewModel, Reservation>]
        SortQuery? sortQuery = null)
        {
            var reservations = await _reservationService.GetAllReservationsAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(reservations);
        }

        [HttpGet("tables")]
        [SwaggerOperation("Get a list of tables for realtime")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<List<TableRealTimeViewModel>>))]
        public async Task<IActionResult> GetAllTablesRealTimeAsync(
            [FromQuery] bool includeDeleted = false
        )
        {
            var tableKeys = await _reservationService.GetAllTablesRealTimeAsync(includeDeleted);
            return Response(tableKeys);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a reservation by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ReservationViewModel>))]
        public async Task<IActionResult> GetReservationByIdAsync([FromRoute] int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            return Response(reservation);
        }

        [HttpGet("{tableNumber}/{storeId}")]
        [SwaggerOperation("Get a reservation by table number and store id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ReservationViewModel>))]
        public async Task<IActionResult> GetReservationByTableNumberAndStoreIdAsync(int tableNumber, Guid storeId)
        {
            var reservation = await _reservationService.GetReservationByTableNumberAndStoreIdAsync(tableNumber, storeId);
            return Response(reservation);
        }

        [HttpPost]
        [SwaggerOperation("Create a new reservation")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateReservationAsync([FromBody] CreateReservationViewModel viewModel)
        {
            var reservationId = await _reservationService.CreateReservationAsync(viewModel);
            return Response(reservationId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a reservation")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteReservationAsync([FromRoute] int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return Response(id);
        }

        [HttpPut]
        [SwaggerOperation("Update a reservation")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateReservationViewModel>))]
        public async Task<IActionResult> UpdateReservationAsync([FromBody] UpdateReservationViewModel viewModel)
        {
            await _reservationService.UpdateReservationAsync(viewModel);
            return Response(viewModel);
        }

        [HttpPut("customer")]
        [SwaggerOperation("Update customer information")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateReservationCustomerViewModel>))]
        public async Task<IActionResult> UpdateReservationCustomerAsync([FromBody] UpdateReservationCustomerViewModel viewModel)
        {
            int customerId = await _reservationService.UpdateReservationCustomerAsync(viewModel);
            return Response(customerId);
        }
    }
}

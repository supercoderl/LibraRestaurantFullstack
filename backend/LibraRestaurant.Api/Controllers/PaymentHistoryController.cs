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
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using LibraRestaurant.Application.ViewModels.PaymentHistorys;
using LibraRestaurant.Domain.Entities;
using System;
using Microsoft.AspNetCore.Authorization;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class PaymentHistoryController : ApiController
    {
        private readonly IPaymentHistoryService _paymentHistoryService;

        public PaymentHistoryController(
            INotificationHandler<DomainNotification> notifications,
            IPaymentHistoryService paymentHistoryService) : base(notifications)
        {
            _paymentHistoryService = paymentHistoryService;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("Get a list of all payment histories")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<PaymentHistoryViewModel>>))]
        public async Task<IActionResult> GetAllMenusAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<PaymentHistoryViewModelSortProvider, PaymentHistoryViewModel, PaymentHistory>]
        SortQuery? sortQuery = null)
        {
            var paymentHistories = await _paymentHistoryService.GetAllPaymentHistoriesAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(paymentHistories);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a payment history by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PaymentHistoryViewModel>))]
        public async Task<IActionResult> GetPaymentHistoryByIdAsync([FromRoute] int id)
        {
            var paymentHistory = await _paymentHistoryService.GetPaymentHistoryByIdAsync(id);
            return Response(paymentHistory);
        }

        [HttpPost]
        [SwaggerOperation("Create a new payment history")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreatePaymentHistoryAsync([FromBody] CreatePaymentHistoryViewModel viewModel)
        {
            try
            {
                var paymentHistoryId = await _paymentHistoryService.CreatePaymentHistoryAsync(viewModel);
                return Response(paymentHistoryId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a payment history")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeletePaymentHistoryAsync([FromRoute] int id)
        {
            await _paymentHistoryService.DeletePaymentHistoryAsync(id);
            return Response(id);
        }
    }
}

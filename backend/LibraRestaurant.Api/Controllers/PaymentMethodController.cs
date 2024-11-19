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
using LibraRestaurant.Application.ViewModels.PaymentMethods;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    /*    [Authorize]*/
    [Route("/api/v1/[controller]")]
    public sealed class PaymentMethodController : ApiController
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(
            INotificationHandler<DomainNotification> notifications,
            IPaymentMethodService paymentMethodService) : base(notifications)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all paymentMethods")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<PaymentMethodViewModel>>))]
        public async Task<IActionResult> GetAllPaymentMethodsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<PaymentMethodViewModelSortProvider, PaymentMethodViewModel, PaymentMethod>]
        SortQuery? sortQuery = null)
        {
            var paymentMethods = await _paymentMethodService.GetAllPaymentMethodsAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(paymentMethods);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a paymentMethod by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PaymentMethodViewModel>))]
        public async Task<IActionResult> GetPaymentMethodByIdAsync([FromRoute] int id)
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethodByIdAsync(id);
            return Response(paymentMethod);
        }

        [HttpPost]
        [SwaggerOperation("Create a new paymentMethod")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreatePaymentMethodAsync([FromBody] CreatePaymentMethodViewModel viewModel)
        {
            var paymentMethodId = await _paymentMethodService.CreatePaymentMethodAsync(viewModel);
            return Response(paymentMethodId);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a paymentMethod")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeletePaymentMethodAsync([FromRoute] int id)
        {
            await _paymentMethodService.DeletePaymentMethodAsync(id);
            return Response(id);
        }

        [HttpPut]
        [SwaggerOperation("Update a paymentMethod")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdatePaymentMethodViewModel>))]
        public async Task<IActionResult> UpdatePaymentMethodAsync([FromBody] UpdatePaymentMethodViewModel viewModel)
        {
            await _paymentMethodService.UpdatePaymentMethodAsync(viewModel);
            return Response(viewModel);
        }
    }
}

using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Customers;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    /*    [Authorize]*/
    [Route("/api/v1/[controller]")]
    public sealed class CustomerController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(
            INotificationHandler<DomainNotification> notifications,
            ICustomerService customerService) : base(notifications)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all customers")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CustomerViewModel>>))]
        public async Task<IActionResult> GetAllCustomersAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<CustomerViewModelSortProvider, CustomerViewModel, Customer>]
        SortQuery? sortQuery = null)
        {
            var currencies = await _customerService.GetAllCustomersAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(currencies);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a customer by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CustomerViewModel>))]
        public async Task<IActionResult> GetCustomerByIdAsync([FromRoute] int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            return Response(customer);
        }

        [HttpPost]
        [SwaggerOperation("Create a new customer")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerViewModel viewModel)
        {
            var customerId = await _customerService.CreateCustomerAsync(viewModel);
            return Response(customerId);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a customer")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteCustomerAsync([FromRoute] int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return Response(id);
        }

        [HttpPut]
        [SwaggerOperation("Update a customer")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateCustomerViewModel>))]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerViewModel viewModel)
        {
            await _customerService.UpdateCustomerAsync(viewModel);
            return Response(viewModel);
        }
    }
}

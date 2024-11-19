using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.EmployeeRoles;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Discounts;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class DiscountController : ApiController
    {
        private readonly IDiscountService _discountService;

        public DiscountController(
            INotificationHandler<DomainNotification> notifications,
            IDiscountService discountService) : base(notifications)
        {
            _discountService = discountService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all discounts")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<DiscountViewModel>>))]
        public async Task<IActionResult> GetAllDiscountsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<DiscountViewModelSortProvider, DiscountViewModel, Discount>]
        SortQuery? sortQuery = null)
        {
            var discounts = await _discountService.GetAllDiscountsAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(discounts);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a discount by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<DiscountViewModel>))]
        public async Task<IActionResult> GetDiscountByIdAsync([FromRoute] int id)
        {
            var discount = await _discountService.GetDiscountByIdAsync(id);
            return Response(discount);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("Create a new discount")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateDiscountAsync([FromBody] CreateDiscountViewModel viewModel)
        {
            var discountId = await _discountService.CreateDiscountAsync(viewModel);
            return Response(discountId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a discount")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteDiscountAsync([FromRoute] int id)
        {
            await _discountService.DeleteDiscountAsync(id);
            return Response(id);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("Update a discount")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateDiscountViewModel>))]
        public async Task<IActionResult> UpdateDiscountAsync([FromBody] UpdateDiscountViewModel viewModel)
        {
            await _discountService.UpdateDiscountAsync(viewModel);
            return Response(viewModel);
        }
    }
}

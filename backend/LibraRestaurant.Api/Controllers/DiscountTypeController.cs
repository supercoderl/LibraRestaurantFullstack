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
using LibraRestaurant.Application.ViewModels.DiscountTypes;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class DiscountTypeController : ApiController
    {
        private readonly IDiscountTypeService _discountTypeService;

        public DiscountTypeController(
            INotificationHandler<DomainNotification> notifications,
            IDiscountTypeService discountTypeService) : base(notifications)
        {
            _discountTypeService = discountTypeService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all discount types")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<DiscountTypeViewModel>>))]
        public async Task<IActionResult> GetAllRolesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<DiscountTypeViewModelSortProvider, DiscountTypeViewModel, DiscountType>]
        SortQuery? sortQuery = null)
        {
            var discountTypes = await _discountTypeService.GetAllDiscountTypesAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(discountTypes);
        }

        [HttpGet("code")]
        [SwaggerOperation("Get a discount type by counpon code")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<DiscountTypeViewModel>))]
        public async Task<IActionResult> GetDiscountTypeByCodeAsync([FromQuery] string counponCode)
        {
            var discountType = await _discountTypeService.GetDiscountTypeByCodeAsync(counponCode);
            return Response(discountType);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a discount type by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<DiscountTypeViewModel>))]
        public async Task<IActionResult> GetDiscountTypeByIdAsync([FromRoute] int id)
        {
            var discountType = await _discountTypeService.GetDiscountTypeByIdAsync(id);
            return Response(discountType);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("Create a new discount type")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateDiscountTypeAsync([FromBody] CreateDiscountTypeViewModel viewModel)
        {
            var discountTypeId = await _discountTypeService.CreateDiscountTypeAsync(viewModel);
            return Response(discountTypeId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a discount type")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteDiscountTypeAsync([FromRoute] int id)
        {
            await _discountTypeService.DeleteDiscountTypeAsync(id);
            return Response(id);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("Update a discount type")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateDiscountTypeViewModel>))]
        public async Task<IActionResult> UpdateDiscountTypeAsync([FromBody] UpdateDiscountTypeViewModel viewModel)
        {
            await _discountTypeService.UpdateDiscountTypeAsync(viewModel);
            return Response(viewModel);
        }
    }
}

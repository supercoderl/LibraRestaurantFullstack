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
using LibraRestaurant.Application.ViewModels.Reviews;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class ReviewController : ApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(
            INotificationHandler<DomainNotification> notifications,
            IReviewService reviewService) : base(notifications)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all reviews")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ReviewViewModel>>))]
        public async Task<IActionResult> GetAllReviewsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] int? itemId = null,
            [FromQuery] [SortableFieldsAttribute<ReviewViewModelSortProvider, ReviewViewModel, Review>]
        SortQuery? sortQuery = null)
        {
            var reviews = await _reviewService.GetAllReviewsAsync(
                query,
                includeDeleted,
                searchTerm,
                itemId,
                sortQuery);
            return Response(reviews);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a review by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ReviewViewModel>))]
        public async Task<IActionResult> GetReviewByIdAsync([FromRoute] int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            return Response(review);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("Create a new review")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateReviewAsync([FromBody] CreateReviewViewModel viewModel)
        {
            var reviewId = await _reviewService.CreateReviewAsync(viewModel);
            return Response(reviewId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a review")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteReviewAsync([FromRoute] int id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return Response(id);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("Update a review")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateReviewViewModel>))]
        public async Task<IActionResult> UpdateReviewAsync([FromBody] UpdateReviewViewModel viewModel)
        {
            await _reviewService.UpdateReviewAsync(viewModel);
            return Response(viewModel);
        }
    }
}

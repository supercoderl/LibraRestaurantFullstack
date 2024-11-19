using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class FeedController : ApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMenuItemService _menuItemService;

        public FeedController(
            INotificationHandler<DomainNotification> notifications,
            ICategoryService categoryService,
            IMenuItemService menuItemService) : base(notifications)
        {
            _categoryService = categoryService;
            _menuItemService = menuItemService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all categories and items")]
        [SwaggerResponse(200, "Request successful")]
        public async Task<IActionResult> GetAllMenusAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync(
                new PageQuery(),
                false,
                true,
                string.Empty,
                null);

            var menuItems = await _menuItemService.GetAllItemsAsync(

                new PageQuery
                {
                    Page = 1,
                    PageSize = 20
                },
                false,
                string.Empty,
                null);
            return Response(new
            {
                categories, menuItems
            });
        }
    }
}

using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController] 
    [Route("/api/v1/[controller]")]
    public sealed class MenuController : ApiController
    {
        private readonly IMenuService _menuService;

        public MenuController(
            INotificationHandler<DomainNotification> notifications,
            IMenuService menuService) : base(notifications)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all menus")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<MenuViewModel>>))]
        public async Task<IActionResult> GetAllMenusAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<MenuViewModelSortProvider, MenuViewModel, Menu>]
        SortQuery? sortQuery = null)
        {
            var menus = await _menuService.GetAllMenusAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(menus);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a menu by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<MenuViewModel>))]
        public async Task<IActionResult> GetMenuByIdAsync([FromRoute] int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            return Response(menu);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("Create a new menu")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateMenuAsync([FromBody] CreateMenuViewModel viewModel)
        {
            var menuId = await _menuService.CreateMenuAsync(viewModel);
            return Response(menuId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a menu")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteMenuAsync([FromRoute] int id)
        {
            await _menuService.DeleteMenuAsync(id);
            return Response(id);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("Update a menu")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateMenuViewModel>))]
        public async Task<IActionResult> UpdateMenuAsync([FromBody] UpdateMenuViewModel viewModel)
        {
            await _menuService.UpdateMenuAsync(viewModel);
            return Response(viewModel);
        }
    }
}

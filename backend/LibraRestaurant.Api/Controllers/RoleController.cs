using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Application.ViewModels.EmployeeRoles;

namespace LibraRestaurant.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class RoleController : ApiController
    {
        private readonly IRoleService _roleService;

        public RoleController(
            INotificationHandler<DomainNotification> notifications,
            IRoleService roleService) : base(notifications)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all roles")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<RoleViewModel>>))]
        public async Task<IActionResult> GetAllRolesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<RoleViewModelSortProvider, RoleViewModel, Role>]
        SortQuery? sortQuery = null)
        {
            var roles = await _roleService.GetAllRolesAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(roles);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a role by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<RoleViewModel>))]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return Response(role);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("Create a new role")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleViewModel viewModel)
        {
            var roleId = await _roleService.CreateRoleAsync(viewModel);
            return Response(roleId);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation("Delete a role")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<int>))]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] int id)
        {
            await _roleService.DeleteRoleAsync(id);
            return Response(id);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("Update a role")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateRoleViewModel>))]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleViewModel viewModel)
        {
            await _roleService.UpdateRoleAsync(viewModel);
            return Response(viewModel);
        }

        [HttpPost("assign")]
        [Authorize]
        [SwaggerOperation("Assign role")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<AssignRoleViewModel>))]
        public async Task<IActionResult> AssignRoleAsync([FromBody] AssignRoleViewModel viewModel)
        {
            await _roleService.AssignRoleAsync(viewModel);
            return Response(viewModel);
        }
    }
}

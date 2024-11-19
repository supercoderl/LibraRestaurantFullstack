using System;
using System.Threading.Tasks;
using LibraRestaurant.Api.Models;
using LibraRestaurant.Api.Swagger;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Employees;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;
using Swashbuckle.AspNetCore.Annotations;

namespace LibraRestaurant.Api.Controllers;

[ApiController]
/*[Authorize]*/
[Route("/api/v1/[controller]")]
public sealed class EmployeeController : ApiController
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(
        INotificationHandler<DomainNotification> notifications,
        IEmployeeService employeeService) : base(notifications)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all employees")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<EmployeeViewModel>>))]
    public async Task<IActionResult> GetAllEmployeesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<EmployeeViewModelSortProvider, EmployeeViewModel, Employee>]
        SortQuery? sortQuery = null)
    {
        var employees = await _employeeService.GetAllEmployeesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(employees);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a employee by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<EmployeeViewModel>))]
    public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] Guid id)
    {
        var employee = await _employeeService.GetEmployeeByEmployeeIdAsync(id);
        return Response(employee);
    }

    [HttpGet("me")]
    [SwaggerOperation("Get the current active employee")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<EmployeeViewModel>))]
    public async Task<IActionResult> GetCurrentEmployeeAsync()
    {
        var employee = await _employeeService.GetCurrentEmployeeAsync();
        return Response(employee);
    }

    [HttpPost]
    [SwaggerOperation("Create a new employee")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeViewModel viewModel)
    {
/*        var password = BC.HashPassword("Password123!");*/
        var employeeId = await _employeeService.CreateEmployeeAsync(viewModel);
        return Response(employeeId);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete a employee")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] Guid id)
    {
        await _employeeService.DeleteEmployeeAsync(id);
        return Response(id);
    }

    [HttpPut]
    [SwaggerOperation("Update a employee")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateEmployeeViewModel>))]
    public async Task<IActionResult> UpdateEmployeeAsync([FromBody] UpdateEmployeeViewModel viewModel)
    {
        await _employeeService.UpdateEmployeeAsync(viewModel);
        return Response(viewModel);
    }

    [HttpPost("changePassword")]
    [SwaggerOperation("Change a password for the current active employee")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ChangePasswordViewModel>))]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordViewModel viewModel)
    {
        await _employeeService.ChangePasswordAsync(viewModel);
        return Response(viewModel);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation("Get a signed token for a employee")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
    public async Task<IActionResult> LoginEmployeeAsync([FromBody] LoginEmployeeViewModel viewModel)
    {
        var token = await _employeeService.LoginEmployeeAsync(viewModel);
        return Response(token);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [SwaggerOperation("Use old token to generate new token")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
    public async Task<IActionResult> RefreshEmployeeAsync([FromBody] RefreshTokenViewModel viewModel)
    {
        var token = await _employeeService.RefreshEmployeeAsync(viewModel.RefreshToken);
        return Response(token);
    }

    [HttpPut("logout")]
    [SwaggerOperation("Revoke token when user logged out or ...")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
    public async Task<IActionResult> LogoutAsync([FromBody] RefreshTokenViewModel viewModel)
    {
        string result = await _employeeService.LogoutAsync(viewModel.RefreshToken);
        return Response(result);
    }
}
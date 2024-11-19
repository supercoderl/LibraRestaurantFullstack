using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.Queries.Employees.GetAll;
using LibraRestaurant.Application.Queries.Employees.GetEmployeeById;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Employees;
using LibraRestaurant.Domain.Commands.Employees.ChangePassword;
using LibraRestaurant.Domain.Commands.Employees.CreateEmployee;
using LibraRestaurant.Domain.Commands.Employees.DeleteEmployee;
using LibraRestaurant.Domain.Commands.Employees.LoginEmployee;
using LibraRestaurant.Domain.Commands.Employees.UpdateEmployee;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Commands.Employees.RefreshEmployee;
using LibraRestaurant.Domain.Commands.Employees.LogoutEmployee;

namespace LibraRestaurant.Application.Services;

public sealed class EmployeeService : IEmployeeService
{
    private readonly IMediatorHandler _bus;
    private readonly IEmployee _employee;

    public EmployeeService(IMediatorHandler bus, IEmployee employee)
    {
        _bus = bus;
        _employee = employee;
    }

    public async Task<EmployeeViewModel?> GetEmployeeByEmployeeIdAsync(Guid employeeId)
    {
        return await _bus.QueryAsync(new GetEmployeeByIdQuery(employeeId));
    }

    public async Task<EmployeeViewModel?> GetCurrentEmployeeAsync()
    {
        return await _bus.QueryAsync(new GetEmployeeByIdQuery(_employee.GetEmployeeId()));
    }

    public async Task<PagedResult<EmployeeViewModel>> GetAllEmployeesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllEmployeesQuery(query, includeDeleted, searchTerm, sortQuery));
    }

    public async Task<Guid> CreateEmployeeAsync(CreateEmployeeViewModel employee)
    {
        var employeeId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateEmployeeCommand(
            employeeId,
            employee.StoreId,
            NormalizeEmail(employee.Email),
            employee.FirstName,
            employee.LastName,
            employee.Mobile,
            "Password123!"));

        return employeeId;
    }

    public async Task UpdateEmployeeAsync(UpdateEmployeeViewModel employee)
    {
        await _bus.SendCommandAsync(new UpdateEmployeeCommand(
            employee.Id,
            employee.StoreId,
            NormalizeEmail(employee.Email),
            employee.FirstName,
            employee.LastName,
            employee.Status,
            employee.Mobile));
    }

    public async Task DeleteEmployeeAsync(Guid employeeId)
    {
        await _bus.SendCommandAsync(new DeleteEmployeeCommand(employeeId));
    }

    public async Task ChangePasswordAsync(ChangePasswordViewModel viewModel)
    {
        await _bus.SendCommandAsync(new ChangePasswordCommand(viewModel.Password, viewModel.NewPassword));
    }

    public async Task<Object> LoginEmployeeAsync(LoginEmployeeViewModel viewModel)
    {
        return await _bus.QueryAsync(new LoginEmployeeCommand(NormalizeEmail(viewModel.Email), viewModel.Password));
    }

    public async Task<Object> RefreshEmployeeAsync(string refreshToken)
    {
        return await _bus.QueryAsync(new RefreshEmployeeCommand(refreshToken));
    }

    public async Task<string> LogoutAsync(string refreshToken)
    {
        return await _bus.QueryAsync(new LogoutEmployeeCommand(refreshToken));
    }

    private string NormalizeEmail(string email)
    {
        var parts = email.Split('@');
        if (parts.Length != 2)
        {
            return email;
        }

        var localPart = parts[0].Replace(".", "");
        var domain = parts[1];

        return $"{localPart}@{domain}";
    }
}
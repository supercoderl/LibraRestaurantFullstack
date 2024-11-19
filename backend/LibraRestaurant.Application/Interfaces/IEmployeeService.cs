using System;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Employees;

namespace LibraRestaurant.Application.Interfaces;

public interface IEmployeeService
{
    public Task<EmployeeViewModel?> GetEmployeeByEmployeeIdAsync(Guid employeeId);
    public Task<EmployeeViewModel?> GetCurrentEmployeeAsync();

    public Task<PagedResult<EmployeeViewModel>> GetAllEmployeesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);

    public Task<Guid> CreateEmployeeAsync(CreateEmployeeViewModel employee);
    public Task UpdateEmployeeAsync(UpdateEmployeeViewModel employee);
    public Task DeleteEmployeeAsync(Guid employeeId);
    public Task ChangePasswordAsync(ChangePasswordViewModel viewModel);
    public Task<Object> LoginEmployeeAsync(LoginEmployeeViewModel viewModel);
    public Task<Object> RefreshEmployeeAsync(string refreshToken);
    public Task<string> LogoutAsync(string refreshToken);
}
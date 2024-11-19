using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Employees;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Application.Queries.Employees.GetAll;

public sealed class GetAllEmployeesQueryHandler :
    IRequestHandler<GetAllEmployeesQuery, PagedResult<EmployeeViewModel>>
{
    private readonly ISortingExpressionProvider<EmployeeViewModel, Employee> _sortingExpressionProvider;
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeesQueryHandler(
        IEmployeeRepository employeeRepository,
        ISortingExpressionProvider<EmployeeViewModel, Employee> sortingExpressionProvider)
    {
        _employeeRepository = employeeRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<EmployeeViewModel>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employeesQuery = _employeeRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            employeesQuery = employeesQuery.Where(employee =>
                employee.Email.Contains(request.SearchTerm) ||
                employee.FirstName.Contains(request.SearchTerm) ||
                employee.LastName.Contains(request.SearchTerm));
        }

        var totalCount = await employeesQuery.CountAsync(cancellationToken);

        employeesQuery = employeesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var employees = await employeesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(employee => EmployeeViewModel.FromEmployee(employee))
            .ToListAsync(cancellationToken);

        return new PagedResult<EmployeeViewModel>(
            totalCount, employees, request.Query.Page, request.Query.PageSize);
    }
}
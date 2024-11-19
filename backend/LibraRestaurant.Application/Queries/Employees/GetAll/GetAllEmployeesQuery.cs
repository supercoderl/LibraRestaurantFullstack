using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Employees;
using MediatR;

namespace LibraRestaurant.Application.Queries.Employees.GetAll;

public sealed record GetAllEmployeesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<EmployeeViewModel>>;
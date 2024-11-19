using System;
using LibraRestaurant.Application.ViewModels.Employees;
using MediatR;

namespace LibraRestaurant.Application.Queries.Employees.GetEmployeeById;

public sealed record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeViewModel?>;
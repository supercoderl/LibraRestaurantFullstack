using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Employees;
using LibraRestaurant.Domain.Entities;

namespace LibraRestaurant.Application.SortProviders;

public sealed class EmployeeViewModelSortProvider : ISortingExpressionProvider<EmployeeViewModel, Employee>
{
    private static readonly Dictionary<string, Expression<Func<Employee, object>>> s_expressions = new()
    {
        { "email", employee => employee.Email },
        { "firstName", employee => employee.FirstName },
        { "lastName", employee => employee.LastName },
        { "mobile", employee => employee.Mobile },
        { "lastloggedindate", employee => employee.LastLoggedinDate ?? DateTimeOffset.MinValue },
        { "status", employee => employee.Status }
    };

    public Dictionary<string, Expression<Func<Employee, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}
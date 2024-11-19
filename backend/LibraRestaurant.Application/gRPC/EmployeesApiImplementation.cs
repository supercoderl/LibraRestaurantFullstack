using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Interfaces.Repositories;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using LibraRestaurant.Proto.Employees;

namespace LibraRestaurant.Application.gRPC;

public sealed class EmployeesApiImplementation : EmployeesApi.EmployeesApiBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesApiImplementation(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public override async Task<GetEmployeesByIdsResult> GetByIds(
        GetEmployeesByIdsRequest request,
        ServerCallContext context)
    {
        var idsAsGuids = new List<Guid>(request.Ids.Count);

        foreach (var id in request.Ids)
        {
            if (Guid.TryParse(id, out var parsed))
            {
                idsAsGuids.Add(parsed);
            }
        }

        var employees = await _employeeRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(employee => idsAsGuids.Contains(employee.Id))
            .Select(employee => new GrpcEmployee
            {
                Id = employee.Id.ToString(),
                StoreId = employee.StoreId.ToString() ?? string.Empty,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                IsDeleted = employee.Deleted
            })
            .ToListAsync();

        var result = new GetEmployeesByIdsResult();

        result.Employees.AddRange(employees);

        return result;
    }
}
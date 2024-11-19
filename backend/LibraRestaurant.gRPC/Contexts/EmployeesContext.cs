using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraRestaurant.gRPC.Interfaces;
using LibraRestaurant.Proto.Employees;
using LibraRestaurant.Shared.Employees;

namespace LibraRestaurant.gRPC.Contexts;

public sealed class EmployeesContext : IEmployeesContext
{
    private readonly EmployeesApi.EmployeesApiClient _client;

    public EmployeesContext(EmployeesApi.EmployeesApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<EmployeeViewModel>> GetEmployeesByIds(IEnumerable<Guid> ids)
    {
        var request = new GetEmployeesByIdsRequest();

        request.Ids.AddRange(ids.Select(id => id.ToString()));

        var result = await _client.GetByIdsAsync(request);

        return result.Employees.Select(employee => new EmployeeViewModel(
            Guid.Parse(employee.Id),
            Guid.Parse(employee.StoreId),
            employee.Email,
            employee.FirstName,
            employee.LastName,
            employee.Mobile,
            employee.IsDeleted));
    }
}
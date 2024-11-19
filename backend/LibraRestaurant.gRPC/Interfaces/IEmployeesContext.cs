using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Employees;

namespace LibraRestaurant.gRPC.Interfaces;

public interface IEmployeesContext
{
    Task<IEnumerable<EmployeeViewModel>> GetEmployeesByIds(IEnumerable<Guid> ids);
}
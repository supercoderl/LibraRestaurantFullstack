using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Employees;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Employees.GetEmployeeById;

public sealed class GetEmployeeByIdQueryHandler :
    IRequestHandler<GetEmployeeByIdQuery, EmployeeViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMediatorHandler bus)
    {
        _employeeRepository = employeeRepository;
        _bus = bus;
    }

    public async Task<EmployeeViewModel?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.Id);

        if (employee is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetEmployeeByIdQuery),
                    $"Employee with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return EmployeeViewModel.FromEmployee(employee);
    }
}
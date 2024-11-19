using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using LibraRestaurant.Shared.Events.Employee;
using MediatR;

namespace LibraRestaurant.Domain.Commands.Employees.DeleteEmployee;

public sealed class DeleteEmployeeCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IEmployee _employee;
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEmployeeRepository employeeRepository,
        IEmployee employee) : base(bus, unitOfWork, notifications)
    {
        _employeeRepository = employeeRepository;
        _employee = employee;
    }

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

        if (employee is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no employee with Id {request.EmployeeId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        if (_employee.GetEmployeeId() != request.EmployeeId)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete employee {request.EmployeeId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        _employeeRepository.Remove(employee);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new EmployeeDeletedEvent(request.EmployeeId));
        }
    }
}
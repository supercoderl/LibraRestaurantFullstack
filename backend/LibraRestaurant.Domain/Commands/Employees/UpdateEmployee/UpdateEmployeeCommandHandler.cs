using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using LibraRestaurant.Shared.Events.Employee;
using MediatR;

namespace LibraRestaurant.Domain.Commands.Employees.UpdateEmployee;

public sealed class UpdateEmployeeCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IEmployee _employee;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEmployeeRepository employeeRepository,
        IEmployee employee) : base(bus, unitOfWork, notifications)
    {
        _employeeRepository = employeeRepository;
        _employee = employee;
    }

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
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
                    $"No permission to update employee {request.EmployeeId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        if (request.Email != employee.Email)
        {
            var existingEmployee = await _employeeRepository.GetByEmailAsync(request.Email);

            if (existingEmployee is not null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is already a employee with email {request.Email}",
                        DomainErrorCodes.Employee.AlreadyExists));
                return;
            }
        }

        employee.SetEmail(request.Email);
        employee.SetFirstName(request.FirstName);
        employee.SetLastName(request.LastName);
        employee.SetMobile(request.Mobile);
        employee.SetStore(request.StoreId);
        employee.SetStatus(request.Status); 

        _employeeRepository.Update(employee);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new EmployeeUpdatedEvent(employee.Id));
        }
    }
}
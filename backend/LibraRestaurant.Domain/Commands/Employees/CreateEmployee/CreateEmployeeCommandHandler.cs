using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using LibraRestaurant.Shared.Events.Employee;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace LibraRestaurant.Domain.Commands.Employees.CreateEmployee;

public sealed class CreateEmployeeCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateEmployeeCommand>
{
    private readonly IEmployee _employee;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEmployeeRepository employeeRepository,
        IEmployee employee) : base(bus, unitOfWork, notifications)
    {
        _employeeRepository = employeeRepository;
        _employee = employee;
    }

    public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var currentEmployee = await _employeeRepository.GetByIdAsync(_employee.GetEmployeeId());
        var userRoles = _employee.GetUserRoles();

        if (currentEmployee is null || (!userRoles.Contains(UserRole.Admin) && !userRoles.Contains(UserRole.Manager)))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    "You are not allowed to create employees",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var existingEmployee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

        if (existingEmployee is not null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a employee with Id {request.EmployeeId}",
                    DomainErrorCodes.Employee.AlreadyExists));
            return;
        }

        existingEmployee = await _employeeRepository.GetByEmailAsync(request.Email);

        if (existingEmployee is not null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a employee with email {request.Email}",
                    DomainErrorCodes.Employee.AlreadyExists));
            return;
        }

        var passwordHash = BC.HashPassword(request.Password);

        var employee = new Employee(
            request.EmployeeId,
            request.StoreId,
            request.Email,
            request.FirstName,
            request.LastName,
            request.Mobile,
            passwordHash,
            System.DateTime.Now);

        _employeeRepository.Add(employee);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new EmployeeCreatedEvent(employee.Id));
        }
    }
}
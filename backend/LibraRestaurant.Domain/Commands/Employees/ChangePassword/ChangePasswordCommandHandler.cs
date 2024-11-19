using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using LibraRestaurant.Shared.Events.Employee;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace LibraRestaurant.Domain.Commands.Employees.ChangePassword;

public sealed class ChangePasswordCommandHandler : CommandHandlerBase,
    IRequestHandler<ChangePasswordCommand>
{
    private readonly IEmployee _employee;
    private readonly IEmployeeRepository _employeeRepository;

    public ChangePasswordCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IEmployeeRepository employeeRepository,
        IEmployee employee) : base(bus, unitOfWork, notifications)
    {
        _employeeRepository = employeeRepository;
        _employee = employee;
    }

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var employee = await _employeeRepository.GetByIdAsync(_employee.GetEmployeeId());

        if (employee is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no employee with Id {_employee.GetEmployeeId()}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        if (!BC.Verify(request.Password, employee.Password))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    "The password is incorrect",
                    DomainErrorCodes.Employee.PasswordIncorrect));

            return;
        }

        var passwordHash = BC.HashPassword(request.NewPassword);
        employee.SetPassword(passwordHash);

        _employeeRepository.Update(employee);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new PasswordChangedEvent(employee.Id));
        }
    }
}
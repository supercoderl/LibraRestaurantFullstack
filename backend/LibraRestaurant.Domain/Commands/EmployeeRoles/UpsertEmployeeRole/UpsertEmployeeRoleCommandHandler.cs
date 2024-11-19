
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.CategoryItem;
using LibraRestaurant.Domain.Errors;
using System.Linq;
using System;
using LibraRestaurant.Shared.Events.EmployeeRole;

namespace LibraRestaurant.Domain.Commands.EmployeeRoles.UpsertEmployeeRole
{
    public sealed class UpsertEmployeeRoleCommandHandler : CommandHandlerBase,
        IRequestHandler<UpsertEmployeeRoleCommand>
    {
        private readonly IEmployeeRoleRepository _employeeRoleRepository;

        public UpsertEmployeeRoleCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IEmployeeRoleRepository employeeRoleRepository) : base(bus, unitOfWork, notifications)
        {
            _employeeRoleRepository = employeeRoleRepository;
        }

        public async Task Handle(UpsertEmployeeRoleCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var employeeRoles = await _employeeRoleRepository.GetByEmployeeAsync(request.EmployeeId);

            // Lấy các EmployeeRole cần xóa (không nằm trong danh sách roleIds truyền vào)
            var employeesToDelete = employeeRoles.Where(er => !request.RoleIds.Contains(er.RoleId)).ToList();

            // Xóa các CategoryItem không có trong danh sách truyền vào
            foreach (var employeeToDelete in employeesToDelete)
            {
                _employeeRoleRepository.Remove(employeeToDelete);
            }

            // Kiểm tra các RoleId chưa có trong database và thêm mới
            foreach (var roleId in request.RoleIds)
            {
                var existingEmployee = employeeRoles.FirstOrDefault(er => er.RoleId == roleId);
                if (existingEmployee is null)
                {
                    // Tạo mới EmployeeRole nếu không tồn tại
                    var newEmployeeRole = new Entities.EmployeeRole(
                        0, // Id mới
                        roleId,
                        request.EmployeeId,
                        request.AssigedDate,
                        request.RevokedDate// Thêm các thuộc tính khác nếu cần
                    );

                    _employeeRoleRepository.Add(newEmployeeRole);

                    if (await CommitAsync())
                    {
                        await Bus.RaiseEventAsync(new EmployeeRoleCreatedEvent(newEmployeeRole.EmployeeRoleId));
                    }
                }
            }

            // Commit việc xóa nếu cần
            if (employeesToDelete.Any() && await CommitAsync())
            {
                foreach (var deletedEmployee in employeesToDelete)
                {
                    await Bus.RaiseEventAsync(new EmployeeRoleUpsertEvent(deletedEmployee.EmployeeRoleId));
                }
            }
        }
    }
}

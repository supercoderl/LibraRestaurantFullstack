using LibraRestaurant.Shared.Events.Employee;
using LibraRestaurant.Shared.Events.EmployeeRole;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.EventHandler
{
    public sealed class EmployeeRoleEventHandler :
        INotificationHandler<EmployeeRoleUpsertEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public EmployeeRoleEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(EmployeeRoleUpsertEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

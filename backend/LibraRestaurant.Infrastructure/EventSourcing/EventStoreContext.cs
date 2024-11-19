using System;
using LibraRestaurant.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LibraRestaurant.Infrastructure.EventSourcing;

public sealed class EventStoreContext : IEventStoreContext
{
    private readonly string _correlationId;
    private readonly IEmployee? _employee;

    public EventStoreContext(IEmployee? employee, IHttpContextAccessor? httpContextAccessor)
    {
        _employee = employee;

        if (httpContextAccessor?.HttpContext is null ||
            !httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-CLEAN-ARCHITECTURE-CORRELATION-ID",
                out var id))
        {
            _correlationId = $"internal - {Guid.NewGuid()}";
        }
        else
        {
            _correlationId = id.ToString();
        }
    }

    public string GetCorrelationId()
    {
        return _correlationId;
    }

    public string GetEmployeeEmail()
    {
        return _employee?.GetEmployeeEmail() ?? string.Empty;
    }
}
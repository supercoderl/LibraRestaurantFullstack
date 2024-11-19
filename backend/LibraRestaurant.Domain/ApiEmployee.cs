using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LibraRestaurant.Domain;

public sealed class ApiEmployee : IEmployee
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private string? _name;
    private Guid _employeeId = Guid.Empty;

    public ApiEmployee(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetEmployeeId()
    {
        if (_employeeId != Guid.Empty)
        {
            return _employeeId;
        }

        var claim = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(x => string.Equals(x.Type, ClaimTypes.NameIdentifier));

        if (Guid.TryParse(claim?.Value, out var employeeId))
        {
            _employeeId = employeeId;
            return employeeId;
        }

        throw new ArgumentException("Could not parse employee id to guid");
    }

    public List<UserRole> GetUserRoles()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims
        .Where(x => string.Equals(x.Type, ClaimTypes.Role))
        .Select(x => x.Value)
        .ToList();


        if (claims == null || !claims.Any())
        {
            throw new ArgumentException("No roles found for the user");
        }

        var userRoles = new List<UserRole>();

        foreach (var claim in claims)
        {
            if (Enum.TryParse(claim, out UserRole userRole))
            {
                userRoles.Add(userRole);
            }
            else
            {
                throw new ArgumentException($"Could not parse role {claim}");
            }
        }

        return userRoles;
    }

    public string Name
    {
        get
        {
            if (_name is not null)
            {
                return _name;
            }

            var identity = _httpContextAccessor.HttpContext?.User.Identity;
            if (identity is null)
            {
                _name = string.Empty;
                return string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(identity.Name))
            {
                _name = identity.Name;
                return identity.Name;
            }

            var claim = _httpContextAccessor.HttpContext!.User.Claims
                .FirstOrDefault(c => string.Equals(c.Type, ClaimTypes.Name, StringComparison.OrdinalIgnoreCase))?
                .Value;
            _name = claim ?? string.Empty;
            return _name;
        }
    }

    public string GetEmployeeEmail()
    {
        var claim = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(x => string.Equals(x.Type, ClaimTypes.Email));

        if (!string.IsNullOrWhiteSpace(claim?.Value))
        {
            return claim.Value;
        }

        return string.Empty;
    }
}
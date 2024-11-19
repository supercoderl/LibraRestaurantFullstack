using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Roles;

public sealed class RoleViewModel
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public static RoleViewModel FromRole(Role role)
    {
        return new RoleViewModel
        {
            RoleId = role.RoleId,
            Name = role.Name,
            Description = role.Description,
        };
    }
}
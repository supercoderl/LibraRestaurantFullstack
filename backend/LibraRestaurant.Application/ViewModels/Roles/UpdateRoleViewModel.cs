using System;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Roles;

public sealed record UpdateRoleViewModel(
    int RoleId,
    string Name,
    string? Description);
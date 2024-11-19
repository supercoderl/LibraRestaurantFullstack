using System;

namespace LibraRestaurant.Application.ViewModels.Roles;

public sealed record CreateRoleViewModel(
    int RoleId,
    string Name,
    string? Description);
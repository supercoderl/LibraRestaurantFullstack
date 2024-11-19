using System;

namespace LibraRestaurant.Shared.Roles;

public sealed record RoleViewModel(
    int RoleId,
    string Name,
    string? Description);
using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using MediatR;

namespace LibraRestaurant.Application.Queries.Roles.GetRoleById;

public sealed record GetRoleByIdQuery(int Id) : IRequest<RoleViewModel?>;
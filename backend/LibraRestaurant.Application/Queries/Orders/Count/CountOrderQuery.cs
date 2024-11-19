using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Orders;
using MediatR;

namespace LibraRestaurant.Application.Queries.Orders.CountOrder;

public sealed record CountOrderQuery(int? Month, int? Year) : IRequest<int>;
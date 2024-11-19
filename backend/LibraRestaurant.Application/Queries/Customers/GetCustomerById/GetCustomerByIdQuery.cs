using System;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Customers;
using LibraRestaurant.Application.ViewModels.Menus;
using MediatR;

namespace LibraRestaurant.Application.Queries.Customers.GetCustomerById;

public sealed record GetCustomerByIdQuery(int Id) : IRequest<CustomerViewModel?>;
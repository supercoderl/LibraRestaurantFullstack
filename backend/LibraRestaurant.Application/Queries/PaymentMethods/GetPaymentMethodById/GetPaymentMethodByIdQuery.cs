using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentMethods;
using MediatR;

namespace LibraRestaurant.Application.Queries.PaymentMethods.GetPaymentMethodById;

public sealed record GetPaymentMethodByIdQuery(int Id) : IRequest<PaymentMethodViewModel?>;
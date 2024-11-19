using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using MediatR;

namespace LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentAmount;

public sealed record GetPaymentAmountQuery() : IRequest<double>;
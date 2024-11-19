using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using MediatR;

namespace LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentHistoryById;

public sealed record GetPaymentHistoryByIdQuery(int Id) : IRequest<PaymentHistoryViewModel?>;
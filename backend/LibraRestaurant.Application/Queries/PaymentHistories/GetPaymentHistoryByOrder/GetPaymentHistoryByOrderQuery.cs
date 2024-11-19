using System;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using MediatR;

namespace LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentHistoryByOrder;

public sealed record GetPaymentHistoryByOrderQuery(Guid OrderId) : IRequest<PaymentHistoryViewModel?>;
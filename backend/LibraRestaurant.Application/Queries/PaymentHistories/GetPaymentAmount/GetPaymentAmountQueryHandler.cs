using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentAmount;

public sealed class GetPaymentAmountQueryHandler :
    IRequestHandler<GetPaymentAmountQuery, double>
{
    private readonly IMediatorHandler _bus;
    private readonly IPaymentHistoryRepository _paymentHistoryRepository;

    public GetPaymentAmountQueryHandler(IPaymentHistoryRepository paymentHistoryRepository, IMediatorHandler bus)
    {
        _paymentHistoryRepository = paymentHistoryRepository;
        _bus = bus;
    }

    public async Task<double> Handle(GetPaymentAmountQuery request, CancellationToken cancellationToken)
    {
        var paymentAmount = await _paymentHistoryRepository.GetPaymentAmount();

        return paymentAmount;
    }
}
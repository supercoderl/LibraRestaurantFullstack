using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentHistoryByOrder;

public sealed class GetPaymentHistoryByOrderQueryHandler :
    IRequestHandler<GetPaymentHistoryByOrderQuery, PaymentHistoryViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IPaymentHistoryRepository _paymentHistoryRepository;

    public GetPaymentHistoryByOrderQueryHandler(IPaymentHistoryRepository paymentHistoryRepository, IMediatorHandler bus)
    {
        _paymentHistoryRepository = paymentHistoryRepository;
        _bus = bus;
    }

    public async Task<PaymentHistoryViewModel?> Handle(GetPaymentHistoryByOrderQuery request, CancellationToken cancellationToken)
    {
        var paymentHistory = await _paymentHistoryRepository.GetByOrderAsync(request.OrderId);

        if (paymentHistory is null)
        {
            return null;
        }

        return PaymentHistoryViewModel.FromPaymentHistory(paymentHistory);
    }
}
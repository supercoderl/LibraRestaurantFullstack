using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentHistoryById;

public sealed class GetPaymentHistoryByIdQueryHandler :
    IRequestHandler<GetPaymentHistoryByIdQuery, PaymentHistoryViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IPaymentHistoryRepository _paymentHistoryRepository;

    public GetPaymentHistoryByIdQueryHandler(IPaymentHistoryRepository paymentHistoryRepository, IMediatorHandler bus)
    {
        _paymentHistoryRepository = paymentHistoryRepository;
        _bus = bus;
    }

    public async Task<PaymentHistoryViewModel?> Handle(GetPaymentHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var paymentHistory = await _paymentHistoryRepository.GetByIdAsync(request.Id);

        if (paymentHistory is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetPaymentHistoryByIdQuery),
                    $"Payment history with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return PaymentHistoryViewModel.FromPaymentHistory(paymentHistory);
    }
}
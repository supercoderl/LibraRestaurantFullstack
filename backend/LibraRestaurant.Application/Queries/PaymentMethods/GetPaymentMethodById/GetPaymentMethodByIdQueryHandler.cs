using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.PaymentMethods;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.PaymentMethods.GetPaymentMethodById;

public sealed class GetPaymentMethodByIdQueryHandler :
    IRequestHandler<GetPaymentMethodByIdQuery, PaymentMethodViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public GetPaymentMethodByIdQueryHandler(IPaymentMethodRepository paymentMethodRepository, IMediatorHandler bus)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _bus = bus;
    }

    public async Task<PaymentMethodViewModel?> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var paymentMethod = await _paymentMethodRepository.GetByIdAsync(request.Id);

        if (paymentMethod is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetPaymentMethodByIdQuery),
                    $"PaymentMethod with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return PaymentMethodViewModel.FromPaymentMethod(paymentMethod);
    }
}
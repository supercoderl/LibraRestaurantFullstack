using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.Queries.Currencies.GetCurrencyById;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Categories.GetCurrencyById;

public sealed class GetCurrencyByIdQueryHandler :
    IRequestHandler<GetCurrencyByIdQuery, CurrencyViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICurrencyRepository _currencyRepository;

    public GetCurrencyByIdQueryHandler(ICurrencyRepository currencyRepository, IMediatorHandler bus)
    {
        _currencyRepository = currencyRepository;
        _bus = bus;
    }

    public async Task<CurrencyViewModel?> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
    {
        var currency = await _currencyRepository.GetByIdAsync(request.Id);

        if (currency is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCurrencyByIdQuery),
                    $"Currency with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return CurrencyViewModel.FromCurrency(currency);
    }
}
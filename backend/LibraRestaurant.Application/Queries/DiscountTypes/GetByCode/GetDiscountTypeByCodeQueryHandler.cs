using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.DiscountTypes;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.DiscountTypes.GetDiscountTypeByCode;

public sealed class GetDiscountTypeByCodeQueryHandler :
    IRequestHandler<GetDiscountTypeByCodeQuery, DiscountTypeViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IDiscountTypeRepository _discountTypeRepository;

    public GetDiscountTypeByCodeQueryHandler(IDiscountTypeRepository discountTypeRepository, IMediatorHandler bus)
    {
        _discountTypeRepository = discountTypeRepository;
        _bus = bus;
    }

    public async Task<DiscountTypeViewModel?> Handle(GetDiscountTypeByCodeQuery request, CancellationToken cancellationToken)
    {
        var discountType = await _discountTypeRepository.GetByCodeAsync(request.CounponCode);

        if (discountType is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetDiscountTypeByCodeQuery),
                    $"Discount type with counpon code {request.CounponCode} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return DiscountTypeViewModel.FromDiscountType(discountType);
    }
}
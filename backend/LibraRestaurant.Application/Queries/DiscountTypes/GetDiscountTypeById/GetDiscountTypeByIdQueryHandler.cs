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

namespace LibraRestaurant.Application.Queries.DiscountTypes.GetDiscountTypeById;

public sealed class GetDiscountTypeByIdQueryHandler :
    IRequestHandler<GetDiscountTypeByIdQuery, DiscountTypeViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IDiscountTypeRepository _discountTypeRepository;

    public GetDiscountTypeByIdQueryHandler(IDiscountTypeRepository discountTypeRepository, IMediatorHandler bus)
    {
        _discountTypeRepository = discountTypeRepository;
        _bus = bus;
    }

    public async Task<DiscountTypeViewModel?> Handle(GetDiscountTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var discountType = await _discountTypeRepository.GetByIdAsync(request.Id);

        if (discountType is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetDiscountTypeByIdQuery),
                    $"Discount type with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return DiscountTypeViewModel.FromDiscountType(discountType);
    }
}
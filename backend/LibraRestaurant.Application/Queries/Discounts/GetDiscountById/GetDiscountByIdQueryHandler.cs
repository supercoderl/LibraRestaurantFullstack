using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Discounts;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Discounts.GetDiscountById;

public sealed class GetDiscountByIdQueryHandler :
    IRequestHandler<GetDiscountByIdQuery, DiscountViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountByIdQueryHandler(IDiscountRepository discountRepository, IMediatorHandler bus)
    {
        _discountRepository = discountRepository;
        _bus = bus;
    }

    public async Task<DiscountViewModel?> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(request.Id);

        if (discount is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetDiscountByIdQuery),
                    $"Discount with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return DiscountViewModel.FromDiscount(discount);
    }
}
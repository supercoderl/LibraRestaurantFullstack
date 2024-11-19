using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Application.ViewModels.Categories;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Notifications;
using MediatR;

namespace LibraRestaurant.Application.Queries.Categories.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler :
    IRequestHandler<GetCategoryByIdQuery, CategoryViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMediatorHandler bus)
    {
        _categoryRepository = categoryRepository;
        _bus = bus;
    }

    public async Task<CategoryViewModel?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        if (category is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCategoryByIdQuery),
                    $"Category with id {request.Id} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return CategoryViewModel.FromCategory(category);
    }
}
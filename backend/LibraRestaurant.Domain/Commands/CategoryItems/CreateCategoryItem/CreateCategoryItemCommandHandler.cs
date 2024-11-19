
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.Category;
using LibraRestaurant.Shared.Events.CategoryItem;

namespace LibraRestaurant.Domain.Commands.CategoryItems.CreateCategoryItem
{
    public sealed class CreateCategoryItemCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateCategoryItemCommand>
    {
        private readonly ICategoryItemRepository _categoryItemRepository;

        public CreateCategoryItemCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICategoryItemRepository categoryItemRepository) : base(bus, unitOfWork, notifications)
        {
            _categoryItemRepository = categoryItemRepository;
        }

        public async Task Handle(CreateCategoryItemCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var categoryItem = new Entities.CategoryItem(
                request.CategoryItemId,
                request.CategoryId,
                request.ItemId,
                request.Description);

            _categoryItemRepository.Add(categoryItem);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CategoryItemCreatedEvent(categoryItem.CategoryItemId));
            }
        }
    }
}

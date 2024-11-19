
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.Category;

namespace LibraRestaurant.Domain.Commands.Categories.CreateCategory
{
    public sealed class CreateCategoryCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICategoryRepository categoryRepository) : base(bus, unitOfWork, notifications)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var category = new Entities.Category(
                request.CategoryId,
                request.Name,
                request.Description,
                request.IsActive,
                request.Picture);

            _categoryRepository.Add(category);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CategoryCreatedEvent(category.CategoryId));
            }
        }
    }
}


using LibraRestaurant.Domain.Errors;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Category;
using LibraRestaurant.Shared.Events.CategoryItem;

namespace LibraRestaurant.Domain.Commands.CategoryItems.DeleteCategoryItem
{
    public sealed class DeleteCategoryItemCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteCategoryItemCommand>
    {
        private readonly ICategoryItemRepository _categoryItemRepository;

        public DeleteCategoryItemCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICategoryItemRepository categoryItemRepository) : base(bus, unitOfWork, notifications)
        {
            _categoryItemRepository = categoryItemRepository;
        }

        public async Task Handle(DeleteCategoryItemCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var categoryItem = await _categoryItemRepository.GetByIdAsync(request.CategoryItemId);

            if (categoryItem is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no category item with Id {request.CategoryItemId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            _categoryItemRepository.Remove(categoryItem);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CategoryItemDeletedEvent(request.CategoryItemId));
            }
        }
    }
}

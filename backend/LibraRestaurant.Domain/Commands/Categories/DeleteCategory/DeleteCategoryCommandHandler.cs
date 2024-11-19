
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

namespace LibraRestaurant.Domain.Commands.Categories.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICategoryRepository categoryRepository) : base(bus, unitOfWork, notifications)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no category with Id {request.CategoryId}",
                        ErrorCodes.ObjectNotFound));

                return;
            }

            _categoryRepository.Remove(category);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CategoryDeletedEvent(request.CategoryId));
            }
        }
    }
}

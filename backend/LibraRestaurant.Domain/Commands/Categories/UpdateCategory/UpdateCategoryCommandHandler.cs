
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
using LibraRestaurant.Shared.Events.MenuItem;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.Category;

namespace LibraRestaurant.Domain.Commands.Categories.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICategoryRepository categoryRepository) : base(bus, unitOfWork, notifications)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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

            category.SetName(request.Name);
            category.SetDescription(request.Description);
            category.SetActive(request.IsActive);
            category.SetPicture(request.Picture);

            _categoryRepository.Update(category);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CategoryUpdatedEvent(category.CategoryId));
            }
        }
    }
}

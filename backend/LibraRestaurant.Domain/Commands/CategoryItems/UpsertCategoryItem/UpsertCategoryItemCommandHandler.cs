
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Shared.Events.CategoryItem;
using LibraRestaurant.Domain.Errors;
using System.Linq;
using System;

namespace LibraRestaurant.Domain.Commands.CategoryItems.UpsertCategoryItem
{
    public sealed class UpsertCategoryItemCommandHandler : CommandHandlerBase,
        IRequestHandler<UpsertCategoryItemCommand>
    {
        private readonly ICategoryItemRepository _categoryItemRepository;

        public UpsertCategoryItemCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICategoryItemRepository categoryItemRepository) : base(bus, unitOfWork, notifications)
        {
            _categoryItemRepository = categoryItemRepository;
        }

        public async Task Handle(UpsertCategoryItemCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var categoryItems = await _categoryItemRepository.GetByItemAsync(request.ItemId);

            // Lấy các CategoryItem cần xóa (không nằm trong danh sách categoryIds truyền vào)
            var itemsToDelete = categoryItems.Where(ci => !request.CategoryIds.Contains(ci.CategoryId)).ToList();

            // Xóa các CategoryItem không có trong danh sách truyền vào
            foreach (var itemToDelete in itemsToDelete)
            {
                _categoryItemRepository.Remove(itemToDelete);
            }

            // Kiểm tra các CategoryId chưa có trong database và thêm mới
            foreach (var categoryId in request.CategoryIds)
            {
                var existingItem = categoryItems.FirstOrDefault(ci => ci.CategoryId == categoryId);
                if (existingItem is null)
                {
                    // Tạo mới CategoryItem nếu không tồn tại
                    var newCategoryItem = new Entities.CategoryItem(
                        0, // Id mới
                        categoryId,
                        request.ItemId,
                        request.Description // Thêm các thuộc tính khác nếu cần
                    );

                    _categoryItemRepository.Add(newCategoryItem);

                    if (await CommitAsync())
                    {
                        await Bus.RaiseEventAsync(new CategoryItemCreatedEvent(newCategoryItem.CategoryItemId));
                    }
                }
            }

            // Commit việc xóa nếu cần
            if (itemsToDelete.Any() && await CommitAsync())
            {
                foreach (var deletedItem in itemsToDelete)
                {
                    await Bus.RaiseEventAsync(new CategoryItemDeletedEvent(deletedItem.CategoryItemId));
                }
            }
        }
    }
}

using LibraRestaurant.Application.Queries.MenuItems.GetAll;
using LibraRestaurant.Application.Queries.Employees.GetAll;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Tests.Fixtures.Queries.Items
{
    public sealed class GetAllItemsTestFixture : QueryHandlerBaseFixture
    {
        private IMenuItemRepository ItemRepository { get; }
        public GetAllItemsQueryHandler Handler { get; }
        public int ExistingItemId { get; } = 0;

        public GetAllItemsTestFixture()
        {
            ItemRepository = Substitute.For<IMenuItemRepository>();
            var sortingProvider = new ItemViewModelSortProvider();

            Handler = new GetAllItemsQueryHandler(ItemRepository, sortingProvider);
        }

        public MenuItem SetupItemAsync()
        {
            var item = new MenuItem(
                ExistingItemId,
                "Test",
                "Test",
                "Test",
                "Test",
                null,
                0,
                0,
                "Test",
                "Test",
                DateTime.Now,
                null);

            var query = new[] { item }.BuildMock();

            ItemRepository.GetAllNoTracking().Returns(query);

            return item;
        }

        public void SetupDeletedItemAsync()
        {
            var item = new MenuItem(
                ExistingItemId,
                "Test",
                "Test",
                "Test",
                "Test",
                null,
                0,
                0,
                "Test",
                "Test",
                DateTime.Now,
                null);

            item.Delete();

            var query = new[] { item }.BuildMock();

            ItemRepository.GetAllNoTracking().Returns(query);
        }
    }
}

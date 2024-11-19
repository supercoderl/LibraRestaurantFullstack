using LibraRestaurant.Application.Queries.MenuItems.GetById;
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
    public sealed class GetItemByIdTestFixture : QueryHandlerBaseFixture
    {
        private IMenuItemRepository ItemRepository { get; }
        public GetItemByIdQueryHandler Handler { get; }
        public int ExistingItemId { get; } = 0;

        public GetItemByIdTestFixture()
        {
            ItemRepository = Substitute.For<IMenuItemRepository>();

            Handler = new GetItemByIdQueryHandler(ItemRepository, Bus);
        }

        public void SetupItemAsync()
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

            ItemRepository.GetByIdAsync(Arg.Is<int>(y => y == ExistingItemId)).Returns(item);
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

            var query = new[] { item }.AsQueryable().BuildMock();

            ItemRepository.GetAllNoTracking().Returns(query);
        }
    }
}

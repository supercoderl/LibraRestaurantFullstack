using LibraRestaurant.Application.gRPC;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.gRPC.Tests.Fixtures
{
    public sealed class ItemTestFixture
    {
        private IMenuItemRepository ItemRepository { get; } = Substitute.For<IMenuItemRepository>();

        public ItemsApiImplementation ItemsApiImplementation { get; }

        public IEnumerable<MenuItem> ExistingItems { get; }

        public ItemTestFixture()
        {
            ExistingItems = new List<MenuItem>
        {
            new(
                0,
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
                null),
            new(
                0,
                "Test1",
                "Test1",
                "Test1",
                "Test1",
                null,
                0,
                0,
                "Test1",
                "Test1",
                DateTime.Now,
                null),
            new(
                0,
                "Test2",
                "Test2",
                "Test2",
                "Test2",
                null,
                0,
                0,
                "Test2",
                "Test2",
                DateTime.Now,
                null)
        };

            var queryable = ExistingItems.BuildMock();

            ItemRepository.GetAllNoTracking().Returns(queryable);

            ItemsApiImplementation = new ItemsApiImplementation(ItemRepository);
        }
    }
}

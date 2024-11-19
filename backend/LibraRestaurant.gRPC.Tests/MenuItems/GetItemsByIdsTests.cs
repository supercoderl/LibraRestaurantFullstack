using FluentAssertions;
using LibraRestaurant.gRPC.Tests.Fixtures;
using LibraRestaurant.Proto.MenuItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraRestaurant.gRPC.Tests.MenuItems
{
    public sealed class GetItemsByIdsTests : IClassFixture<ItemTestFixture>
    {
        private readonly ItemTestFixture _fixture;

        public GetItemsByIdsTests(ItemTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Get_Empty_List_If_No_Ids_Are_Given()
        {
            var result = await _fixture.ItemsApiImplementation.GetByIds(
                SetupRequest(Enumerable.Empty<int>()),
                default!);

            result.Items.Should().HaveCount(0);
        }

        [Fact]
        public async Task Should_Get_Requested_Items()
        {
            var nonExistingId = 0;

            var ids = _fixture.ExistingItems
                .Take(2)
                .Select(item => item.ItemId)
                .ToList();

            ids.Add(nonExistingId);

            var result = await _fixture.ItemsApiImplementation.GetByIds(
                SetupRequest(ids),
                default!);

            result.Items.Should().HaveCount(2);

            foreach (var item in result.Items)
            {
                var ItemId = item.Id;

                ItemId.Should().NotBe(nonExistingId);

                var mockItem = _fixture.ExistingItems.First(u => u.ItemId == ItemId);

                mockItem.Should().NotBeNull();

                item.Title.Should().Be(mockItem.Title);
                item.Slug.Should().Be(mockItem.Slug);
                item.Summary.Should().Be(mockItem.Summary);
                item.Sku.Should().Be(mockItem.SKU);
                item.Price.Should().Be(mockItem.Price);
                item.Quantity.Should().Be(mockItem.Quantity);
                item.Recipe.Should().Be(mockItem.Recipe);
                item.Instruction.Should().Be(mockItem.Instruction);
            }
        }

        private static GetItemsByIdsRequest SetupRequest(IEnumerable<int> ids)
        {
            var request = new GetItemsByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id));

            return request;
        }
    }
}

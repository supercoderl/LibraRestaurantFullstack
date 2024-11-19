
using FluentAssertions;
using LibraRestaurant.Application.Queries.MenuItems.GetAll;
using LibraRestaurant.Application.Tests.Fixtures.Queries.Items;
using LibraRestaurant.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraRestaurant.Application.Tests.Queries.MenuItems
{
    public sealed class GetAllItemsQueryHandlerTests
    {
        private readonly GetAllItemsTestFixture _fixture = new();

        [Fact]
        public async Task Should_Get_All_Items()
        {
            var item = _fixture.SetupItemAsync();

            var query = new PageQuery
            {
                PageSize = 1,
                Page = 1
            };

            var result = await _fixture.Handler.Handle(
                new GetAllItemsQuery(query, false, item.Title),
                default);

            _fixture.VerifyNoDomainNotification();

            result.PageSize.Should().Be(query.PageSize);
            result.Page.Should().Be(query.Page);
            result.Count.Should().Be(1);

            var ItemViewModels = result.Items.ToArray();
            ItemViewModels.Should().NotBeNull();
            ItemViewModels.Should().ContainSingle();
            ItemViewModels.FirstOrDefault()!.ItemId.Should().Be(_fixture.ExistingItemId);
        }

        [Fact]
        public async Task Should_Not_Get_Deleted_Items()
        {
            _fixture.SetupDeletedItemAsync();

            var query = new PageQuery
            {
                PageSize = 10,
                Page = 1
            };

            var result = await _fixture.Handler.Handle(
                new GetAllItemsQuery(query, false),
                default);

            _fixture.VerifyNoDomainNotification();

            result.PageSize.Should().Be(query.PageSize);
            result.Page.Should().Be(query.Page);
            result.Count.Should().Be(0);

            result.Items.Should().BeEmpty();
        }
    }
}

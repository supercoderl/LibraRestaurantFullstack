
using FluentAssertions;
using LibraRestaurant.Application.Queries.MenuItems.GetById;
using LibraRestaurant.Application.Tests.Fixtures.Queries.Items;
using LibraRestaurant.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraRestaurant.Application.Tests.Queries.MenuItems
{
    public sealed class GetItemByIdQueryHandlerTests
    {
        private readonly GetItemByIdTestFixture _fixture = new();

        [Fact]
        public async Task Should_Get_Existing_Item()
        {
            _fixture.SetupItemAsync();

            var result = await _fixture.Handler.Handle(
                new GetItemByIdQuery(_fixture.ExistingItemId),
                default);

            _fixture.VerifyNoDomainNotification();

            result.Should().NotBeNull();
            result!.ItemId.Should().Be(_fixture.ExistingItemId);
        }

        [Fact]
        public async Task Should_Raise_Notification_For_No_Item()
        {
            _fixture.SetupItemAsync();

            var request = new GetItemByIdQuery(0);
            var result = await _fixture.Handler.Handle(
                request,
                default);

            _fixture.VerifyExistingNotification(
                nameof(GetItemByIdQuery),
                ErrorCodes.ObjectNotFound,
                $"Item with id {request.Id} could not be found");

            result.Should().BeNull();
        }

        [Fact]
        public async Task Should_Not_Get_Deleted_Item()
        {
            _fixture.SetupDeletedItemAsync();

            var result = await _fixture.Handler.Handle(
                new GetItemByIdQuery(_fixture.ExistingItemId),
                default);

            _fixture.VerifyExistingNotification(
                nameof(GetItemByIdQuery),
                ErrorCodes.ObjectNotFound,
                $"Item with id {_fixture.ExistingItemId} could not be found");

            result.Should().BeNull();
        }
    }
}

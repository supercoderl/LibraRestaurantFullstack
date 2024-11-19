using FluentAssertions;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.MenuItems;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.IntegrationTests.Extensions;
using LibraRestaurant.IntegrationTests.Fixtures;
using LibraRestaurant.IntegrationTests.Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace LibraRestaurant.IntegrationTests.Controller
{
    [Collection("IntegrationTests")]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public sealed class ItemControllerTests : IClassFixture<ItemTestFixture>
    {
        private readonly ItemTestFixture _fixture;

        public ItemControllerTests(ItemTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Priority(0)]
        public async Task Should_Get_All_Item()
        {
            var response = await _fixture.ServerClient.GetAsync("/api/v1/item");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var message = await response.Content.ReadAsJsonAsync<PagedResult<ItemViewModel>>();

            message?.Data.Should().NotBeNull();

            var content = message!.Data!.Items.ToList();

            content.Count.Should().Be(2);
        }

        [Fact]
        [Priority(5)]
        public async Task Should_Get_User_By_Id()
        {
            var response = await _fixture.ServerClient.GetAsync("/api/v1/item/" + 0);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var message = await response.Content.ReadAsJsonAsync<ItemViewModel>();

            message?.Data.Should().NotBeNull();

            var content = message!.Data!;
        }

        [Fact]
        [Priority(10)]
        public async Task Should_Create_Item()
        {
            var item = new CreateItemViewModel(
                "Test",
                "Test",
                "Test",
                "Test",
                0,
                0,
                "Test",
                "Test",
                null,
                null);

            var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/item", item);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var message = await response.Content.ReadAsJsonAsync<int>();
        }

        [Fact]
        [Priority(25)]
        public async Task Should_Update_Item()
        {
            var item = new UpdateItemViewModel(
                Ids.Seed.NumberId,
                "Test",
                "Test",
                "Test",
                "Test",
                0,
                0,
                "Test",
                "Test",
                null,
                null,
                null);

            var response = await _fixture.ServerClient.PutAsJsonAsync("/api/v1/item", item);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var message = await response.Content.ReadAsJsonAsync<UpdateItemViewModel>();

            message?.Data.Should().NotBeNull();

            var content = message!.Data;

            content.Should().BeEquivalentTo(item);

            // Check if item is really updated
            var itemResponse = await _fixture.ServerClient.GetAsync("/api/v1/item/" + item.ItemId);

            itemResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var itemMessage = await itemResponse.Content.ReadAsJsonAsync<ItemViewModel>();

            itemMessage?.Data.Should().NotBeNull();

            var itemContent = itemMessage!.Data!;

            itemContent.ItemId.Should().Be(item.ItemId);
            itemContent.Title.Should().Be(item.Title);
            itemContent.Slug.Should().Be(item.Slug);
            itemContent.Summary.Should().Be(item.Summary);
            itemContent.SKU.Should().Be(item.SKU);
            itemContent.Price.Should().Be(item.Price);
            itemContent.Quantity.Should().Be(item.Quantity);
            itemContent.Recipe.Should().Be(item.Recipe);
            itemContent.Instruction.Should().Be(item.Instruction);
        }

        [Fact]
        [Priority(35)]
        public async Task Should_Delete_Item()
        {
            var response = await _fixture.ServerClient.DeleteAsync("/api/v1/item/" + 0);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var message = await response.Content.ReadAsJsonAsync<int>();

            message?.Data.Should();

            var content = message!.Data;
            content.Should().Be(0);

            var itemResponse = await _fixture.ServerClient.GetAsync("/api/v1/item/" + 0);

            itemResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}

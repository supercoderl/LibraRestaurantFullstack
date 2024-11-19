using LibraRestaurant.IntegrationTests.Fixtures.gRPC;
using LibraRestaurant.Proto.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Priority;
using Xunit;
using LibraRestaurant.Proto.MenuItems;
using FluentAssertions;

namespace LibraRestaurant.IntegrationTests.gRPC
{
    [Collection("IntegrationTests")]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public sealed class GetItemsByIdsTests : IClassFixture<GetItemsByIdsTestFixture>
    {
        private readonly GetItemsByIdsTestFixture _fixture;

        public GetItemsByIdsTests(GetItemsByIdsTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Get_Items_By_Ids()
        {
            var client = new ItemsApi.ItemsApiClient(_fixture.GrpcChannel);

            var request = new GetItemsByIdsRequest();
            request.Ids.Add(_fixture.CreatedItemId);

            var response = await client.GetByIdsAsync(request);

            response.Items.Should().HaveCount(1);

            var item = response.Items.First();
            var createdItem = _fixture.CreateItem();

            item.Title.Should().Be(createdItem.Title);
            item.Slug.Should().Be(createdItem.Slug);
            item.Summary.Should().Be(createdItem.Summary);
            item.Sku.Should().Be(createdItem.SKU);
            item.Price.Should().Be(createdItem.Price);
            item.Quantity.Should().Be(createdItem.Quantity);
            item.Recipe.Should().Be(createdItem.Recipe);
            item.Instruction.Should().Be(createdItem.Instruction);
        }
    }
}

using Grpc.Net.Client;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.IntegrationTests.Fixtures.gRPC
{
    public sealed class GetItemsByIdsTestFixture : TestFixtureBase
    {
        public GrpcChannel GrpcChannel { get; }
        public int CreatedItemId { get; } = 0;

        public GetItemsByIdsTestFixture()
        {
            GrpcChannel = GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
            {
                HttpHandler = Factory.Server.CreateHandler()
            });
        }

        protected override void SeedTestData(ApplicationDbContext context)
        {
            base.SeedTestData(context);

            var item = CreateItem();

            context.MenuItems.Add(item);
            context.SaveChanges();
        }

        public MenuItem CreateItem()
        {
            return new MenuItem(
                CreatedItemId,
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
        }
    }
}

using System;
using LibraRestaurant.Domain.Constants;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Infrastructure.Database;
using Grpc.Net.Client;

namespace LibraRestaurant.IntegrationTests.Fixtures.gRPC;

public sealed class GetUsersByIdsTestFixture : TestFixtureBase
{
    public GrpcChannel GrpcChannel { get; }
    public Guid CreatedUserId { get; } = Guid.NewGuid();

    public GetUsersByIdsTestFixture()
    {
        GrpcChannel = GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            HttpHandler = Factory.Server.CreateHandler()
        });
    }

    protected override void SeedTestData(ApplicationDbContext context)
    {
        base.SeedTestData(context);

        var user = CreateUser();

        context.Employees.Add(user);
        context.SaveChanges();
    }

    public Employee CreateUser()
    {
        return new Employee(
            CreatedUserId,
            null,
            "user@user.de",
            "User First Name",
            "User Last Name",
            "09091234567",
            "User Password",
            DateTime.Now);
    }
}
using LibraRestaurant.IntegrationTests.Fixtures;
using Xunit;

namespace LibraRestaurant.IntegrationTests;

[CollectionDefinition("IntegrationTests", DisableParallelization = true)]
public sealed class IntegrationTestsCollection :
    ICollectionFixture<AccessorFixture>
{
}
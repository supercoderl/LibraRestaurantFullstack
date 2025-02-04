using System;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Infrastructure.Database;
using LibraRestaurant.Infrastructure.Tests.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace LibraRestaurant.Infrastructure.Tests;

public sealed class UnitOfWorkTests
{
    [Fact]
    public async Task Should_Commit_Async_Returns_True()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        var dbContextMock = Substitute.For<ApplicationDbContext>(options.Options);
        var loggerMock = Substitute.For<ILogger<UnitOfWork<ApplicationDbContext>>>();

        dbContextMock.SaveChangesAsync(CancellationToken.None).Returns(Task.FromResult(1));

        var unitOfWork = UnitOfWorkTestFixture.GetUnitOfWork(dbContextMock, loggerMock);

        var result = await unitOfWork.CommitAsync();

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Should_Commit_Async_Returns_False()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        var dbContextMock = Substitute.For<ApplicationDbContext>(options.Options);
        var loggerMock = Substitute.For<ILogger<UnitOfWork<ApplicationDbContext>>>();

        dbContextMock
            .When(x => x.SaveChangesAsync(CancellationToken.None))
            .Do(_ => throw new DbUpdateException("Boom", new Exception("it broke")));

        var unitOfWork = UnitOfWorkTestFixture.GetUnitOfWork(dbContextMock, loggerMock);

        var result = await unitOfWork.CommitAsync();

        result.Should().BeFalse();
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Commiting_With_DbUpdateException()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        var dbContextMock = Substitute.For<ApplicationDbContext>(options.Options);
        var loggerMock = Substitute.For<ILogger<UnitOfWork<ApplicationDbContext>>>();

        dbContextMock
            .When(x => x.SaveChangesAsync(CancellationToken.None))
            .Do(_ => throw new Exception("Boom"));

        var unitOfWork = UnitOfWorkTestFixture.GetUnitOfWork(dbContextMock, loggerMock);

        Func<Task> throwsAction = async () => await unitOfWork.CommitAsync();

        await throwsAction.Should().ThrowAsync<Exception>();
    }
}
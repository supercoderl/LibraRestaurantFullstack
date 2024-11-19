using System;
using System.Linq.Expressions;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Domain.Notifications;
using LibraRestaurant.Shared.Events;
using NSubstitute;

namespace LibraRestaurant.Domain.Tests;

public class CommandHandlerFixtureBase
{
    protected IMediatorHandler Bus { get; }
    protected IUnitOfWork UnitOfWork { get; }
    protected DomainNotificationHandler NotificationHandler { get; }
    protected IEmployee Employee { get; }

    protected CommandHandlerFixtureBase()
    {
        Bus = Substitute.For<IMediatorHandler>();
        UnitOfWork = Substitute.For<IUnitOfWork>();
        NotificationHandler = Substitute.For<DomainNotificationHandler>();
        Employee = Substitute.For<IEmployee>();

        Employee.GetEmployeeId().Returns(Guid.NewGuid());

        UnitOfWork.CommitAsync().Returns(true);
    }

    public CommandHandlerFixtureBase VerifyExistingNotification(string errorCode, string message)
    {
        Bus.Received(1).RaiseEventAsync(
            Arg.Is<DomainNotification>(not => not.Code == errorCode && not.Value == message));

        return this;
    }

    public CommandHandlerFixtureBase VerifyAnyDomainNotification()
    {
        Bus.Received(1).RaiseEventAsync(Arg.Any<DomainNotification>());

        return this;
    }

    public CommandHandlerFixtureBase VerifyNoDomainNotification()
    {
        Bus.DidNotReceive().RaiseEventAsync(Arg.Any<DomainNotification>());

        return this;
    }

    public CommandHandlerFixtureBase VerifyNoRaisedEvent<TEvent>()
        where TEvent : DomainEvent
    {
        Bus.DidNotReceive().RaiseEventAsync(Arg.Any<TEvent>());

        return this;
    }

    public CommandHandlerFixtureBase VerifyNoRaisedEvent<TEvent>(Expression<Predicate<TEvent>> checkFunction)
        where TEvent : DomainEvent
    {
        Bus.DidNotReceive().RaiseEventAsync(Arg.Is(checkFunction));

        return this;
    }

    public CommandHandlerFixtureBase VerifyNoCommit()
    {
        UnitOfWork.DidNotReceive().CommitAsync();

        return this;
    }

    public CommandHandlerFixtureBase VerifyCommit()
    {
        UnitOfWork.Received(1).CommitAsync();

        return this;
    }

    public CommandHandlerFixtureBase VerifyRaisedEvent<TEvent>(Expression<Predicate<TEvent>> checkFunction)
        where TEvent : DomainEvent
    {
        Bus.Received(1).RaiseEventAsync(Arg.Is(checkFunction));

        return this;
    }
}
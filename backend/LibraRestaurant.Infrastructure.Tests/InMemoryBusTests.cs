using System;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Commands.Employees.DeleteEmployee;
using LibraRestaurant.Domain.Commands.MenuItems.DeleteItem;
using LibraRestaurant.Domain.DomainEvents;
using LibraRestaurant.Domain.EventHandler.Fanout;
using LibraRestaurant.Domain.Notifications;
using LibraRestaurant.Shared.Events.Employee;
using LibraRestaurant.Shared.Events.MenuItem;
using MediatR;
using NSubstitute;
using Xunit;

namespace LibraRestaurant.Infrastructure.Tests;

public sealed class InMemoryBusTests
{
    [Fact]
    public async Task InMemoryBus_Should_Publish_When_Not_DomainNotification()
    {
        var mediator = Substitute.For<IMediator>();
        var domainEventStore = Substitute.For<IDomainEventStore>();
        var fanoutEventHandler = Substitute.For<IFanoutEventHandler>();

        var inMemoryBus = new InMemoryBus(mediator, domainEventStore, fanoutEventHandler);

        const string key = "Key";
        const string value = "Value";
        const string code = "Code";

        var domainEvent = new DomainNotification(key, value, code);

        await inMemoryBus.RaiseEventAsync(domainEvent);

        await mediator.Received(1).Publish(Arg.Is<DomainNotification>(x => x.Equals(domainEvent)));
    }

    [Fact]
    public async Task InMemoryBus_Should_Save_And_Publish_When_DomainNotification()
    {
        var mediator = Substitute.For<IMediator>();
        var domainEventStore = Substitute.For<IDomainEventStore>();
        var fanoutEventHandler = Substitute.For<IFanoutEventHandler>();

        var inMemoryBus = new InMemoryBus(mediator, domainEventStore, fanoutEventHandler);

        var userDeletedEvent = new EmployeeDeletedEvent(Guid.NewGuid());

        await inMemoryBus.RaiseEventAsync(userDeletedEvent);

        await mediator.Received(1).Publish(Arg.Is<EmployeeDeletedEvent>(x => x.Equals(userDeletedEvent)));

        var itemDeletedEvent = new ItemDeletedEvent(0);

        await inMemoryBus.RaiseEventAsync(itemDeletedEvent);

        await mediator.Received(1).Publish(Arg.Is<ItemDeletedEvent>(x => x.Equals(itemDeletedEvent)));
    }

    [Fact]
    public async Task InMemoryBus_Should_Send_Command_Async()
    {
        var mediator = Substitute.For<IMediator>();
        var domainEventStore = Substitute.For<IDomainEventStore>();
        var fanoutEventHandler = Substitute.For<IFanoutEventHandler>();

        var inMemoryBus = new InMemoryBus(mediator, domainEventStore, fanoutEventHandler); //remember add fanoutEventHandler

        var deleteUserCommand = new DeleteEmployeeCommand(Guid.NewGuid());

        await inMemoryBus.SendCommandAsync(deleteUserCommand);

        await mediator.Received(1).Send(Arg.Is<DeleteEmployeeCommand>(x => x.Equals(deleteUserCommand)));

        var deleteItemCommand = new DeleteItemCommand(0);

        await inMemoryBus.SendCommandAsync(deleteItemCommand);

        await mediator.Received(1).Send(Arg.Is<DeleteEmployeeCommand>(x => x.Equals(deleteItemCommand)));
    }
}
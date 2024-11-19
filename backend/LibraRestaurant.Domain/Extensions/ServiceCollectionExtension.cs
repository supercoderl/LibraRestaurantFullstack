
using LibraRestaurant.Domain.Commands.Categories.CreateCategory;
using LibraRestaurant.Domain.Commands.Categories.DeleteCategory;
using LibraRestaurant.Domain.Commands.Categories.UpdateCategory;
using LibraRestaurant.Domain.Commands.Currencies.CreateCurrency;
using LibraRestaurant.Domain.Commands.Currencies.DeleteCurrency;
using LibraRestaurant.Domain.Commands.Currencies.UpdateCurrency;
using LibraRestaurant.Domain.Commands.MenuItems.CreateItem;
using LibraRestaurant.Domain.Commands.MenuItems.DeleteItem;
using LibraRestaurant.Domain.Commands.MenuItems.UpdateItem;
using LibraRestaurant.Domain.Commands.Menus.CreateMenu;
using LibraRestaurant.Domain.Commands.Menus.DeleteMenu;
using LibraRestaurant.Domain.Commands.Menus.UpdateMenu;
using LibraRestaurant.Domain.Commands.OrderLines.CreateOrderLine;
using LibraRestaurant.Domain.Commands.OrderLines.DeleteOrderLine;
using LibraRestaurant.Domain.Commands.OrderLines.UpdateOrderLine;
using LibraRestaurant.Domain.Commands.Orders.CreateOrder;
using LibraRestaurant.Domain.Commands.Orders.DeleteOrder;
using LibraRestaurant.Domain.Commands.Orders.UpdateOrder;
using LibraRestaurant.Domain.Commands.PaymentHistories.CreatePaymentHistory;
using LibraRestaurant.Domain.Commands.PaymentHistories.DeletePaymentHistory;
using LibraRestaurant.Domain.Commands.PaymentMethods.CreatePaymentMethod;
using LibraRestaurant.Domain.Commands.PaymentMethods.DeletePaymentMethod;
using LibraRestaurant.Domain.Commands.PaymentMethods.UpdatePaymentMethod;
using LibraRestaurant.Domain.Commands.Reservation.UpdateReservation;
using LibraRestaurant.Domain.Commands.Reservations.CreateReservation;
using LibraRestaurant.Domain.Commands.Reservations.DeleteReservation;
using LibraRestaurant.Domain.Commands.Reservations.UpdateReservation;
using LibraRestaurant.Domain.Commands.Stores.CreateStore;
using LibraRestaurant.Domain.Commands.Stores.DeleteStore;
using LibraRestaurant.Domain.Commands.Stores.UpdateStore;
using LibraRestaurant.Domain.Commands.Employees.ChangePassword;
using LibraRestaurant.Domain.Commands.Employees.CreateEmployee;
using LibraRestaurant.Domain.Commands.Employees.DeleteEmployee;
using LibraRestaurant.Domain.Commands.Employees.LoginEmployee;
using LibraRestaurant.Domain.Commands.Employees.UpdateEmployee;
using LibraRestaurant.Domain.EventHandler;
using LibraRestaurant.Domain.EventHandler.Fanout;
using LibraRestaurant.Domain.Interfaces;
using LibraRestaurant.Shared.Events.Category;
using LibraRestaurant.Shared.Events.Currency;
using LibraRestaurant.Shared.Events.Menu;
using LibraRestaurant.Shared.Events.MenuItem;
using LibraRestaurant.Shared.Events.OrderHead;
using LibraRestaurant.Shared.Events.OrderLine;
using LibraRestaurant.Shared.Events.PaymentHistory;
using LibraRestaurant.Shared.Events.PaymentMethod;
using LibraRestaurant.Shared.Events.Reservation;
using LibraRestaurant.Shared.Events.Store;
using LibraRestaurant.Shared.Events.Employee;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using LibraRestaurant.Domain.Commands.CategoryItems.CreateCategoryItem;
using LibraRestaurant.Domain.Commands.CategoryItems.UpdateCategoryItem;
using LibraRestaurant.Domain.Commands.CategoryItems.DeleteCategoryItem;
using LibraRestaurant.Shared.Events.CategoryItem;
using LibraRestaurant.Domain.Commands.CategoryItems.UpsertCategoryItem;
using LibraRestaurant.Shared.Events.Role;
using LibraRestaurant.Domain.Commands.Roles.CreateRole;
using LibraRestaurant.Domain.Commands.Roles.UpdateRole;
using LibraRestaurant.Domain.Commands.Roles.DeleteRole;
using LibraRestaurant.Domain.Commands.EmployeeRoles.UpsertEmployeeRole;
using LibraRestaurant.Shared.Events.EmployeeRole;
using System;
using LibraRestaurant.Domain.Commands.Tokens.CreateToken;
using LibraRestaurant.Domain.Commands.Tokens.UpdateToken;
using LibraRestaurant.Shared.Events.Token;
using LibraRestaurant.Domain.Commands.Employees.RefreshEmployee;
using LibraRestaurant.Domain.Commands.Messages.SendMessage;
using LibraRestaurant.Shared.Events.Messages;
using LibraRestaurant.Domain.Commands.Messages.UpdateMessage;
using LibraRestaurant.Domain.Commands.Employees.LogoutEmployee;
using LibraRestaurant.Domain.Commands.Reservations.UpdateReservationCustomer;
using LibraRestaurant.Domain.Commands.Reservations.GenerateQRCode;
using LibraRestaurant.Domain.Commands.Discounts.CreateDiscount;
using LibraRestaurant.Domain.Commands.Discounts.UpdateDiscount;
using LibraRestaurant.Domain.Commands.Discounts.DeleteDiscount;
using LibraRestaurant.Domain.Commands.DiscountTypes.CreateDiscountType;
using LibraRestaurant.Domain.Commands.DiscountTypes.UpdateDiscountType;
using LibraRestaurant.Domain.Commands.DiscountTypes.DeleteDiscountType;
using LibraRestaurant.Shared.Events.Discount;
using LibraRestaurant.Shared.Events.DiscountType;
using LibraRestaurant.Domain.Commands.Reviews.CreateReview;
using LibraRestaurant.Domain.Commands.Reviews.DeleteReview;
using LibraRestaurant.Shared.Events.Review;
using LibraRestaurant.Domain.Commands.Reviews.UpdateReview;
using LibraRestaurant.Domain.Commands.Customers.CreateCustomer;
using LibraRestaurant.Domain.Commands.Customers.DeleteCustomer;
using LibraRestaurant.Domain.Commands.Customers.UpdateCustomer;
using LibraRestaurant.Shared.Events.Customer;
using LibraRestaurant.Domain.Commands.Socials.LoginSocial;

namespace LibraRestaurant.Domain.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        // Employee
        services.AddScoped<IRequestHandler<CreateEmployeeCommand>, CreateEmployeeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEmployeeCommand>, UpdateEmployeeCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteEmployeeCommand>, DeleteEmployeeCommandHandler>();
        services.AddScoped<IRequestHandler<ChangePasswordCommand>, ChangePasswordCommandHandler>();
        services.AddScoped<IRequestHandler<LoginEmployeeCommand, Object>, LoginEmployeeCommandHandler>();
        services.AddScoped<IRequestHandler<RefreshEmployeeCommand, Object>, RefreshEmployeeCommandHandler>();
        services.AddScoped<IRequestHandler<LogoutEmployeeCommand, string>, LogoutEmployeeCommandHandler>();

        // Item
        services.AddScoped<IRequestHandler<CreateItemCommand>, CreateItemCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateItemCommand>, UpdateItemCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteItemCommand>, DeleteItemCommandHandler>();

        // Menu
        services.AddScoped<IRequestHandler<CreateMenuCommand>, CreateMenuCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateMenuCommand>, UpdateMenuCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteMenuCommand>, DeleteMenuCommandHandler>();

        // Category
        services.AddScoped<IRequestHandler<CreateCategoryCommand>, CreateCategoryCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCategoryCommand>, UpdateCategoryCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCategoryCommand>, DeleteCategoryCommandHandler>();

        // Currency
        services.AddScoped<IRequestHandler<CreateCurrencyCommand>, CreateCurrencyCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCurrencyCommand>, UpdateCurrencyCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCurrencyCommand>, DeleteCurrencyCommandHandler>();

        // Order
        services.AddScoped<IRequestHandler<CreateOrderCommand>, CreateOrderCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateOrderCommand>, UpdateOrderCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteOrderCommand>, DeleteOrderCommandHandler>();

        // Store
        services.AddScoped<IRequestHandler<CreateStoreCommand>, CreateStoreCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateStoreCommand>, UpdateStoreCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteStoreCommand>, DeleteStoreCommandHandler>();

        // Reservation
        services.AddScoped<IRequestHandler<CreateReservationCommand>, CreateReservationCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateReservationCommand>, UpdateReservationCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteReservationCommand>, DeleteReservationCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateReservationCustomerCommand, int>, UpdateReservationCustomerCommandHandler>();
        services.AddScoped<IRequestHandler<GenerateQRCodeCommand>,  GenerateQRCodeCommandHandler>();

        // OrderLine
        services.AddScoped<IRequestHandler<CreateOrderLineCommand>, CreateOrderLineCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateOrderLineCommand>, UpdateOrderLineCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteOrderLineCommand>, DeleteOrderLineCommandHandler>();

        // PaymentMethod
        services.AddScoped<IRequestHandler<CreatePaymentMethodCommand>, CreatePaymentMethodCommandHandler>();
        services.AddScoped<IRequestHandler<UpdatePaymentMethodCommand>, UpdatePaymentMethodCommandHandler>();
        services.AddScoped<IRequestHandler<DeletePaymentMethodCommand>, DeletePaymentMethodCommandHandler>();

        //PaymentHistory
        services.AddScoped<IRequestHandler<CreatePaymentHistoryCommand>, CreatePaymentHistoryCommandHandler>();
        services.AddScoped<IRequestHandler<DeletePaymentHistoryCommand>, DeletePaymentHistoryCommandHandler>();

        //CategoryItem
        services.AddScoped<IRequestHandler<CreateCategoryItemCommand>, CreateCategoryItemCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCategoryItemCommand>, UpdateCategoryItemCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCategoryItemCommand>, DeleteCategoryItemCommandHandler>();
        services.AddScoped<IRequestHandler<UpsertCategoryItemCommand>, UpsertCategoryItemCommandHandler>();

        //Role
        services.AddScoped<IRequestHandler<CreateRoleCommand>, CreateRoleCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRoleCommand>, UpdateRoleCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteRoleCommand>, DeleteRoleCommandHandler>();

        //EmployeeRole
        services.AddScoped<IRequestHandler<UpsertEmployeeRoleCommand>, UpsertEmployeeRoleCommandHandler>();

        //Token
        services.AddScoped<IRequestHandler<CreateTokenCommand>, CreateTokenCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateTokenCommand>, UpdateTokenCommandHandler>();

        //Message
        services.AddScoped<IRequestHandler<SendMessageCommand>, SendMessageCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateMessageCommand>, UpdateMessageCommandHandler>();

        //Discount
        services.AddScoped<IRequestHandler<CreateDiscountCommand>, CreateDiscountCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateDiscountCommand>, UpdateDiscountCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteDiscountCommand>, DeleteDiscountCommandHandler>();

        //DiscountType
        services.AddScoped<IRequestHandler<CreateDiscountTypeCommand>, CreateDiscountTypeCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateDiscountTypeCommand>, UpdateDiscountTypeCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteDiscountTypeCommand>, DeleteDiscountTypeCommandHandler>();

        //Review
        services.AddScoped<IRequestHandler<CreateReviewCommand>, CreateReviewCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteReviewCommand>, DeleteReviewCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateReviewCommand>, UpdateReviewCommandHandler>();

        //Customer
        services.AddScoped<IRequestHandler<CreateCustomerCommand, int>, CreateCustomerCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCustomerCommand>, DeleteCustomerCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCustomerCommand>, UpdateCustomerCommandHandler>();

        //Social
        services.AddScoped<IRequestHandler<LoginSocialCommand, Object>, LoginSocialCommandHandler>();

        return services;
    }

    public static IServiceCollection AddNotificationHandlers(this IServiceCollection services)
    {
        // Fanout
        services.AddScoped<IFanoutEventHandler, FanoutEventHandler>();

        // User
        services.AddScoped<INotificationHandler<EmployeeCreatedEvent>, EmployeeEventHandler>();
        services.AddScoped<INotificationHandler<EmployeeUpdatedEvent>, EmployeeEventHandler>();
        services.AddScoped<INotificationHandler<EmployeeDeletedEvent>, EmployeeEventHandler>();
        services.AddScoped<INotificationHandler<PasswordChangedEvent>, EmployeeEventHandler>();

        // Item
        services.AddScoped<INotificationHandler<ItemCreatedEvent>, ItemEventHandler>();
        services.AddScoped<INotificationHandler<ItemUpdatedEvent>, ItemEventHandler>();
        services.AddScoped<INotificationHandler<ItemDeletedEvent>, ItemEventHandler>();

        // Menu
        services.AddScoped<INotificationHandler<MenuCreatedEvent>, MenuEventHandler>();
        services.AddScoped<INotificationHandler<MenuUpdatedEvent>, MenuEventHandler>();
        services.AddScoped<INotificationHandler<MenuDeletedEvent>, MenuEventHandler>();

        // Category
        services.AddScoped<INotificationHandler<CategoryCreatedEvent>, CategoryEventHandler>();
        services.AddScoped<INotificationHandler<CategoryUpdatedEvent>, CategoryEventHandler>();
        services.AddScoped<INotificationHandler<CategoryDeletedEvent>, CategoryEventHandler>();

        // Currency
        services.AddScoped<INotificationHandler<CurrencyCreatedEvent>, CurrencyEventHandler>();
        services.AddScoped<INotificationHandler<CurrencyUpdatedEvent>, CurrencyEventHandler>();
        services.AddScoped<INotificationHandler<CurrencyDeletedEvent>, CurrencyEventHandler>();

        // Order
        services.AddScoped<INotificationHandler<OrderCreatedEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<OrderUpdatedEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<OrderDeletedEvent>, OrderEventHandler>();

        // Store
        services.AddScoped<INotificationHandler<StoreCreatedEvent>, StoreEventHandler>();
        services.AddScoped<INotificationHandler<StoreUpdatedEvent>, StoreEventHandler>();
        services.AddScoped<INotificationHandler<StoreDeletedEvent>, StoreEventHandler>();

        // Reservation
        services.AddScoped<INotificationHandler<ReservationCreatedEvent>, ReservationEventHandler>();
        services.AddScoped<INotificationHandler<ReservationUpdatedEvent>, ReservationEventHandler>();
        services.AddScoped<INotificationHandler<ReservationDeletedEvent>, ReservationEventHandler>();

        // OrderLine
        services.AddScoped<INotificationHandler<OrderLineCreatedEvent>, OrderLineEventHandler>();
        services.AddScoped<INotificationHandler<OrderLineUpdatedEvent>, OrderLineEventHandler>();
        services.AddScoped<INotificationHandler<OrderLineDeletedEvent>, OrderLineEventHandler>();

        // PaymentMethod
        services.AddScoped<INotificationHandler<PaymentMethodCreatedEvent>, PaymentMethodEventHandler>();
        services.AddScoped<INotificationHandler<PaymentMethodUpdatedEvent>, PaymentMethodEventHandler>();
        services.AddScoped<INotificationHandler<PaymentMethodDeletedEvent>, PaymentMethodEventHandler>();

        //PaymentHistory
        services.AddScoped<INotificationHandler<PaymentHistoryCreatedEvent>, PaymentHistoryEventHandler>();
        services.AddScoped<INotificationHandler<PaymentHistoryDeletedEvent>, PaymentHistoryEventHandler>();

        //CategoryItem
        services.AddScoped<INotificationHandler<CategoryItemCreatedEvent>, CategoryItemEventHandler>();
        services.AddScoped<INotificationHandler<CategoryItemUpdatedEvent>, CategoryItemEventHandler>();
        services.AddScoped<INotificationHandler<CategoryItemDeletedEvent>, CategoryItemEventHandler>();
        services.AddScoped<INotificationHandler<CategoryItemUpsertEvent>, CategoryItemEventHandler>();

        //Role
        services.AddScoped<INotificationHandler<RoleCreatedEvent>, RoleEventHandler>();
        services.AddScoped<INotificationHandler<RoleUpdatedEvent>, RoleEventHandler>();
        services.AddScoped<INotificationHandler<RoleDeletedEvent>, RoleEventHandler>();

        //EmployeeRole
        services.AddScoped<INotificationHandler<EmployeeRoleUpsertEvent>, EmployeeRoleEventHandler>();

        //Token
        //Role
        services.AddScoped<INotificationHandler<TokenCreatedEvent>, TokenEventHandler>();
        services.AddScoped<INotificationHandler<TokenUpdatedEvent>, TokenEventHandler>();

        //Message
        services.AddScoped<INotificationHandler<MessageSentEvent>, MessageEventHandler>();
        services.AddScoped<INotificationHandler<MessageUpdatedEvent>, MessageEventHandler>();

        //Discount
        services.AddScoped<INotificationHandler<DiscountCreatedEvent>, DiscountEventHandler>();
        services.AddScoped<INotificationHandler<DiscountUpdatedEvent>, DiscountEventHandler>();
        services.AddScoped<INotificationHandler<DiscountDeletedEvent>, DiscountEventHandler>();

        //DiscountType
        services.AddScoped<INotificationHandler<DiscountTypeCreatedEvent>, DiscountTypeEventHandler>();
        services.AddScoped<INotificationHandler<DiscountTypeUpdatedEvent>, DiscountTypeEventHandler>();
        services.AddScoped<INotificationHandler<DiscountTypeDeletedEvent>, DiscountTypeEventHandler>();

        //Review
        services.AddScoped<INotificationHandler<ReviewCreatedEvent>, ReviewEventHandler>();
        services.AddScoped<INotificationHandler<ReviewDeletedEvent>, ReviewEventHandler>();
        services.AddScoped<INotificationHandler<ReviewUpdatedEvent>, ReviewEventHandler>();

        //Customer
        services.AddScoped<INotificationHandler<CustomerCreatedEvent>, CustomerEventHandler>();
        services.AddScoped<INotificationHandler<CustomerDeletedEvent>, CustomerEventHandler>();
        services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();

        return services;
    }

    public static IServiceCollection AddApiEmployee(this IServiceCollection services)
    {
        // User
        services.AddScoped<IEmployee, ApiEmployee>();

        return services;
    }
}
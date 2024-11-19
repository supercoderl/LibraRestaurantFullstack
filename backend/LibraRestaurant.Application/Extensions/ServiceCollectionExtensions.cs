using CloudinaryDotNet;
using LibraRestaurant.Application.Interfaces;
using LibraRestaurant.Application.Queries.Categories.GetAll;
using LibraRestaurant.Application.Queries.Categories.GetCategoryById;
using LibraRestaurant.Application.Queries.Categories.GetCurrencyById;
using LibraRestaurant.Application.Queries.Cities.GetAll;
using LibraRestaurant.Application.Queries.Cities.GetCityById;
using LibraRestaurant.Application.Queries.Currencies.GetAll;
using LibraRestaurant.Application.Queries.Currencies.GetCurrencyById;
using LibraRestaurant.Application.Queries.Districts.GetAll;
using LibraRestaurant.Application.Queries.Districts.GetDistrictById;
using LibraRestaurant.Application.Queries.MenuItems.GetAll;
using LibraRestaurant.Application.Queries.MenuItems.GetById;
using LibraRestaurant.Application.Queries.Menus.GetAll;
using LibraRestaurant.Application.Queries.Menus.GetMenuById;
using LibraRestaurant.Application.Queries.OrderLines.GetAll;
using LibraRestaurant.Application.Queries.OrderLines.GetOrderLineById;
using LibraRestaurant.Application.Queries.OrderLines.GetOrderLineByOrderAndItem;
using LibraRestaurant.Application.Queries.Orders.CountOrder;
using LibraRestaurant.Application.Queries.Orders.GetAll;
using LibraRestaurant.Application.Queries.Orders.GetOrderById;
using LibraRestaurant.Application.Queries.Orders.GetOrderByStoreAndReservation;
using LibraRestaurant.Application.Queries.PaymentHistories.GetAll;
using LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentAmount;
using LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentHistoryById;
using LibraRestaurant.Application.Queries.PaymentHistories.GetPaymentHistoryByOrder;
using LibraRestaurant.Application.Queries.PaymentMethods.GetAll;
using LibraRestaurant.Application.Queries.PaymentMethods.GetPaymentMethodById;
using LibraRestaurant.Application.Queries.Reservations.GetAll;
using LibraRestaurant.Application.Queries.Reservations.GetReservationById;
using LibraRestaurant.Application.Queries.Reservations.GetReservationByTableNumberAndStoreId;
using LibraRestaurant.Application.Queries.Stores.GetAll;
using LibraRestaurant.Application.Queries.Stores.GetStoreById;
using LibraRestaurant.Application.Queries.Employees.GetAll;
using LibraRestaurant.Application.Queries.Employees.GetEmployeeById;
using LibraRestaurant.Application.Queries.Wards.GetAll;
using LibraRestaurant.Application.Queries.Wards.GetWardById;
using LibraRestaurant.Application.Services;
using LibraRestaurant.Application.SortProviders;
using LibraRestaurant.Application.ViewModels;
using LibraRestaurant.Application.ViewModels.Categories;
using LibraRestaurant.Application.ViewModels.Cities;
using LibraRestaurant.Application.ViewModels.Currencies;
using LibraRestaurant.Application.ViewModels.Districts;
using LibraRestaurant.Application.ViewModels.MenuItems;
using LibraRestaurant.Application.ViewModels.Menus;
using LibraRestaurant.Application.ViewModels.OrderLines;
using LibraRestaurant.Application.ViewModels.Orders;
using LibraRestaurant.Application.ViewModels.PaymentHistories;
using LibraRestaurant.Application.ViewModels.PaymentMethods;
using LibraRestaurant.Application.ViewModels.Payments;
using LibraRestaurant.Application.ViewModels.Reservations;
using LibraRestaurant.Application.ViewModels.Sorting;
using LibraRestaurant.Application.ViewModels.Stores;
using LibraRestaurant.Application.ViewModels.Employees;
using LibraRestaurant.Application.ViewModels.Wards;
using LibraRestaurant.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraRestaurant.Application.ViewModels.Roles;
using LibraRestaurant.Application.Queries.Roles.GetRoleById;
using LibraRestaurant.Application.Queries.Roles.GetAll;
using LibraRestaurant.Application.Queries.MenuItems.GetBySlug;
using LibraRestaurant.Application.ViewModels.Settings;
using LibraRestaurant.Application.Queries.Reservations.GetAllTablesRealTime;
using System.Collections.Generic;
using LibraRestaurant.Application.Queries.Messages.GetAll;
using LibraRestaurant.Application.ViewModels.Messages;
using LibraRestaurant.Application.Queries.Discounts.GetDiscountById;
using LibraRestaurant.Application.ViewModels.Discounts;
using LibraRestaurant.Application.Queries.Discounts.GetAll;
using LibraRestaurant.Application.Queries.DiscountTypes.GetDiscountTypeById;
using LibraRestaurant.Application.ViewModels.DiscountTypes;
using LibraRestaurant.Application.Queries.DiscountTypes.GetAll;
using LibraRestaurant.Application.Queries.DiscountTypes.GetDiscountTypeByCode;
using LibraRestaurant.Application.ViewModels.Reviews;
using LibraRestaurant.Application.Queries.Reviews.GetAll;
using LibraRestaurant.Application.Queries.Reviews.GetReviewById;
using LibraRestaurant.Application.Queries.Customers.GetCustomerById;
using LibraRestaurant.Application.ViewModels.Customers;
using LibraRestaurant.Application.Queries.Customers.GetAll;
using LibraRestaurant.Application.Queries.Reservations.GetReservationByStatus;
using LibraRestaurant.Application.Queries.Orders.GetOrdersByPhone;

namespace LibraRestaurant.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Normal Service
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IOrderLineService, OrderLineService>();
        services.AddScoped<IPaypalService, PaypalService>();
        services.AddScoped<IVnPayService,  VnPayService>();
        services.AddScoped<IStripeService, StripeService>();
        services.AddScoped<IPayOsService, PayOsService>();
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IDistrictService, DistrictService>();
        services.AddScoped<IWardService, WardService>();
        services.AddScoped<IPaymentHistoryService, PaymentHistoryService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<ICategoryItemService, CategoryItemService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IRealTimeService, RealTimeService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IDiscountTypeService, DiscountTypeService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ISocialService, SocialService>();

        services.AddSingleton<Cloudinary>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var cloudinaryAccount = new Account(
                configuration["CloudinaryConfiguration:CloudName"],
                configuration["CloudinaryConfiguration:ApiKey"],
                configuration["CloudinaryConfiguration:ApiSecret"]
            );
            return new Cloudinary(cloudinaryAccount);
        });

        services.AddSingleton<PaypalConfig>(pp =>
        {
            var configuration = pp.GetRequiredService<IConfiguration>();
            return new PaypalConfig(
                configuration["PaypalConfiguration:Mode"]!,
                configuration["PaypalConfiguration:ClientID"]!,
                configuration["PaypalConfiguration:ClientSecret"]!
            );
        });

        services.AddSingleton<VNPayConfig>(vnp =>
        {
            var configuration = vnp.GetRequiredService<IConfiguration>();
            return new VNPayConfig(
                configuration["VNPayConfiguration:ReturnURL"]!,
                configuration["VNPayConfiguration:BaseURL"]!,
                configuration["VNPayConfiguration:TmnCode"]!,
                configuration["VNPayConfiguration:HashSecret"]!
            );
        });

        services.AddSingleton<StripeConfig>(s =>
        {
            var configuration = s.GetRequiredService<IConfiguration>();
            return new StripeConfig(
                configuration["StripeConfiguration:ApiKey"]!,
                configuration["StripeConfiguration:SecretKey"]!,
                configuration["StripeConfiguration:ReturnURL"]!,
                configuration["StripeConfiguration:CancelURL"]!
            );
        });

        services.AddSingleton<PayOSConfig>(p =>
        {
            var configuration = p.GetRequiredService<IConfiguration>();
            return new PayOSConfig(
                configuration["PayOSConfiguration:ClientID"]!,
                configuration["PayOSConfiguration:ApiKey"]!,
                configuration["PayOSConfiguration:ChecksumKey"]!,
                configuration["PayOSConfiguration:ReturnURL"]!,
                configuration["PayOSConfiguration:CancelURL"]!
            );
        });

        services.AddSingleton<CoreSettings>(p =>
        {
            var configuration = p.GetRequiredService<IConfiguration>();
            return new CoreSettings(configuration["Core:ServerURL"]!);
        });

        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        // User
        services.AddScoped<IRequestHandler<GetEmployeeByIdQuery, EmployeeViewModel?>, GetEmployeeByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllEmployeesQuery, PagedResult<EmployeeViewModel>>, GetAllEmployeesQueryHandler>();

        // Item
        services.AddScoped<IRequestHandler<GetItemByIdQuery, ItemViewModel?>, GetItemByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllItemsQuery, PagedResult<ItemViewModel>>, GetAllItemsQueryHandler>();
        services.AddScoped<IRequestHandler<GetItemBySlugQuery, ItemViewModel?>, GetItemBySlugQueryHandler>();

        // Menu
        services.AddScoped<IRequestHandler<GetMenuByIdQuery, MenuViewModel?>, GetMenuByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllMenusQuery, PagedResult<MenuViewModel>>, GetAllMenusQueryHandler>();

        // Category
        services.AddScoped<IRequestHandler<GetCategoryByIdQuery, CategoryViewModel?>, GetCategoryByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryViewModel>>, GetAllCategoriesQueryHandler>();

        // Currency
        services.AddScoped<IRequestHandler<GetCurrencyByIdQuery, CurrencyViewModel?>, GetCurrencyByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCurrenciesQuery, PagedResult<CurrencyViewModel>>, GetAllCurrenciesQueryHandler>();

        // Order
        services.AddScoped<IRequestHandler<GetOrderByIdQuery, OrderViewModel?>, GetOrderByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllOrdersQuery, PagedResult<OrderViewModel>>, GetAllOrdersQueryHandler>();
        services.AddScoped<IRequestHandler<GetOrderByStoreAndReservationQuery, OrderViewModel?>, GetOrderByStoreAndReservationQueryHandler>();
        services.AddScoped<IRequestHandler<CountOrderQuery, int>, CountOrderQueryHandler>();
        services.AddScoped<IRequestHandler<GetOrdersByPhoneQuery, PagedResult<OrderViewModel>>, GetOrdersByPhoneQueryHandler>();

        // Store
        services.AddScoped<IRequestHandler<GetStoreByIdQuery, StoreViewModel?>, GetStoreByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllStoresQuery, PagedResult<StoreViewModel>>, GetAllStoresQueryHandler>();

        // Reservation
        services.AddScoped<IRequestHandler<GetReservationByIdQuery, ReservationViewModel?>, GetReservationByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllReservationsQuery, PagedResult<ReservationViewModel>>, GetAllReservationsQueryHandler>();
        services.AddScoped<IRequestHandler<GetReservationByTableNumberAndStoreIdQuery, ReservationViewModel?>, GetReservationByTableNumberAndStoreIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllTablesRealTimeQuery, List<TableRealTimeViewModel>>, GetAllTablesRealTimeQueryHandler>();
        services.AddScoped<IRequestHandler<GetReservationByStatusQuery, List<ReservationViewModel>>, GetReservationByStatusQueryHandler>();

        // OrderLine
        services.AddScoped<IRequestHandler<GetOrderLineByIdQuery, OrderLineViewModel?>, GetOrderLineByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllOrderLinesQuery, PagedResult<OrderLineViewModel>>, GetAllOrderLinesQueryHandler>();
        services.AddScoped<IRequestHandler<GetOrderLineByOrderAndItemQuery, OrderLineViewModel?>, GetOrderLineByOrderAndItemQueryHandler>();

        // PaymentMethod
        services.AddScoped<IRequestHandler<GetPaymentMethodByIdQuery, PaymentMethodViewModel?>, GetPaymentMethodByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllPaymentMethodsQuery, PagedResult<PaymentMethodViewModel>>, GetAllPaymentMethodsQueryHandler>();

        // City
        services.AddScoped<IRequestHandler<GetCityByIdQuery, CityViewModel?>, GetCityByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCitiesQuery, PagedResult<CityViewModel>>, GetAllCitiesQueryHandler>();

        // District
        services.AddScoped<IRequestHandler<GetDistrictByIdQuery, DistrictViewModel?>, GetDistrictByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllDistrictsQuery, PagedResult<DistrictViewModel>>, GetAllDistrictsQueryHandler>();

        // Ward
        services.AddScoped<IRequestHandler<GetWardByIdQuery, WardViewModel?>, GetWardByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllWardsQuery, PagedResult<WardViewModel>>, GetAllWardsQueryHandler>();

        // Payment History
        services.AddScoped<IRequestHandler<GetPaymentHistoryByIdQuery, PaymentHistoryViewModel?>, GetPaymentHistoryByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllPaymentHistoriesQuery, PagedResult<PaymentHistoryViewModel>>, GetAllPaymentHistoriesQueryHandler>();
        services.AddScoped<IRequestHandler<GetPaymentHistoryByOrderQuery, PaymentHistoryViewModel?>, GetPaymentHistoryByOrderQueryHandler>();
        services.AddScoped<IRequestHandler<GetPaymentAmountQuery, double>, GetPaymentAmountQueryHandler>();

        // Role
        services.AddScoped<IRequestHandler<GetRoleByIdQuery, RoleViewModel?>, GetRoleByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllRolesQuery, PagedResult<RoleViewModel>>, GetAllRolesQueryHandler>();

        //Message
        services.AddScoped<IRequestHandler<GetAllMessagesQuery, PagedResult<MessageViewModel>>, GetAllMessagesQueryHandler>();

        // Discount
        services.AddScoped<IRequestHandler<GetDiscountByIdQuery, DiscountViewModel?>, GetDiscountByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllDiscountsQuery, PagedResult<DiscountViewModel>>, GetAllDiscountsQueryHandler>();

        // Discount Type
        services.AddScoped<IRequestHandler<GetDiscountTypeByIdQuery, DiscountTypeViewModel?>, GetDiscountTypeByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllDiscountTypesQuery, PagedResult<DiscountTypeViewModel>>, GetAllDiscountTypesQueryHandler>();
        services.AddScoped<IRequestHandler<GetDiscountTypeByCodeQuery, DiscountTypeViewModel?>, GetDiscountTypeByCodeQueryHandler>();

        // Review
        services.AddScoped<IRequestHandler<GetReviewByIdQuery, ReviewViewModel?>, GetReviewByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllReviewsQuery, PagedResult<ReviewViewModel>>, GetAllReviewsQueryHandler>();

        // Review
        services.AddScoped<IRequestHandler<GetCustomerByIdQuery, CustomerViewModel?>, GetCustomerByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCustomersQuery, PagedResult<CustomerViewModel>>, GetAllCustomersQueryHandler>();

        return services;
    }

    public static IServiceCollection AddSortProviders(this IServiceCollection services)
    {
        services.AddScoped<ISortingExpressionProvider<EmployeeViewModel, Employee>, EmployeeViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<ItemViewModel, MenuItem>, ItemViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<MenuViewModel, Menu>, MenuViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<CategoryViewModel, Category>, CategoryViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<CurrencyViewModel, Currency>, CurrencyViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<OrderViewModel, OrderHeader>, OrderViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<StoreViewModel, Store>, StoreViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<ReservationViewModel, Reservation>, ReservationViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<OrderLineViewModel, OrderLine>, OrderLineViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<PaymentMethodViewModel, Domain.Entities.PaymentMethod>, PaymentMethodViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<CityViewModel, City>, CityViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<DistrictViewModel, District>, DistrictViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<WardViewModel, Ward>, WardViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<PaymentHistoryViewModel, PaymentHistory>, PaymentHistoryViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<RoleViewModel, Role>, RoleViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<MessageViewModel, Message>, MessageViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<DiscountViewModel, Discount>, DiscountViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<DiscountTypeViewModel, DiscountType>, DiscountTypeViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<ReviewViewModel, Review>, ReviewViewModelSortProvider>();
        services.AddScoped<ISortingExpressionProvider<CustomerViewModel, Customer>, CustomerViewModelSortProvider>();

        return services;
    }
}
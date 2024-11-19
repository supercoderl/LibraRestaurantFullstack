using LibraRestaurant.gRPC.Interfaces;

namespace LibraRestaurant.gRPC;

public sealed class LibraRestaurant : ILibraRestaurant
{
    public LibraRestaurant(
        IEmployeesContext employees, IMenuItemsContext items, IMenusContext menus, ICategoriesContext categories, ICurrenciesContext currencies, IOrdersContext orders, IReservationsContext reservations, IOrderLinesContext orderLines, IPaymentMethodsContext paymentMethods, ICitiesContext cities, IDistrictsContext districts, IWardsContext wards, IPaymentHistoriesContext paymentHistories, IRolesContext roles)
    {
        Employees = employees;
        Items = items;
        Menus = menus;
        Currencies = currencies;
        Categories = categories;
        Orders = orders;
        Reservations = reservations;
        OrderLines = orderLines;
        PaymentMethods = paymentMethods;
        Cities = cities;
        IDistricts = districts;
        Wards = wards;
        PaymentHistories = paymentHistories;
        Roles = roles;
    }

    public IEmployeesContext Employees { get; }

    public IMenuItemsContext Items {  get; }

    public IMenusContext Menus { get; }

    public ICategoriesContext Categories { get; }

    public ICurrenciesContext Currencies { get; }

    public IOrdersContext Orders { get; }

    public IReservationsContext Reservations { get; }

    public IOrderLinesContext OrderLines { get; }

    public IPaymentMethodsContext PaymentMethods { get; }

    public ICitiesContext Cities { get; }

    public IDistrictsContext IDistricts { get; }

    public IWardsContext Wards { get; }

    public IPaymentHistoriesContext PaymentHistories { get; }

    public IRolesContext Roles { get; }
}
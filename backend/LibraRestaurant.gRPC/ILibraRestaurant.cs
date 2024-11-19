using LibraRestaurant.gRPC.Interfaces;

namespace LibraRestaurant.gRPC;

public interface ILibraRestaurant
{
    IEmployeesContext Employees { get; }
    IMenuItemsContext Items { get; }
    IMenusContext Menus { get; }
    ICategoriesContext Categories { get; }
    ICurrenciesContext Currencies { get; }
    IOrdersContext Orders { get; }
    IReservationsContext Reservations { get; }
    IOrderLinesContext OrderLines { get; }
    IPaymentMethodsContext PaymentMethods { get; }
    ICitiesContext Cities { get; }
    IDistrictsContext IDistricts { get; }
    IWardsContext Wards { get; }
    IPaymentHistoriesContext PaymentHistories { get; }
    IRolesContext Roles { get; }
}
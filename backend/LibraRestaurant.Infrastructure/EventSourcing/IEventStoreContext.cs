namespace LibraRestaurant.Infrastructure.EventSourcing;

public interface IEventStoreContext
{
    public string GetEmployeeEmail();
    public string GetCorrelationId();
}
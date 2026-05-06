using Core;

public interface IOrderService
{
    Task<List<OrderItems>> GetCustomerOrders(int userId);
}
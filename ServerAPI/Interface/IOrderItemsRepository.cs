using Core;

namespace Interface
{
    public interface IOrderItemsRepository
    {
        Task<List<OrderItems>> GetOrdersByCustomerId(int userId);
        Task<OrderItems?> GetOrderById(int id);

    }
}
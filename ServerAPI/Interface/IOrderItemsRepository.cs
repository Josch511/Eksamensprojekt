using Core;

namespace Interface
{
    public interface IOrderItemsRepository
    {
        Task<OrderItems> GetOrder(int id);
    
    }
}
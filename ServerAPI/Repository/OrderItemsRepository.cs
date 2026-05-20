using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Repository;

public class OrderItemsRepository : IOrderItemsRepository
{
    private readonly IMongoCollection<OrderItems> _orderItems;

    public OrderItemsRepository(AuthenticationRepo authRepo)
    {
        _orderItems = authRepo.db.GetCollection<OrderItems>("orderItems");
    }

    public async Task<List<OrderItems>> GetOrdersByCustomerId(int userId)
    {
        return await _orderItems.Find(o => o.userId == userId).ToListAsync();
    }
    public async Task<OrderItems?> GetOrderById(int id)
    {
        return await _orderItems.Find(o => o._id == id).FirstOrDefaultAsync();
    }
}
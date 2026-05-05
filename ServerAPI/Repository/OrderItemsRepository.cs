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
        _orderItems = authRepo.db.GetCollection<OrderItems>("order_items");
    }

    public async Task<OrderItems> GetOrder(int id)
    {
        return await _orderItems.Find(o => o._id == id).FirstOrDefaultAsync();
    }
}
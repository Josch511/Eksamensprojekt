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
        _orderItems = authRepo.db.GetCollection<OrderItemsCustomer>("order_items");
    }

   
}
using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers;


[ApiController]
[Route("orderItems")]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemsRepository _orderItemsRepository;

    public OrderItemsController(IOrderItemsRepository orderItemsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
    }


    [HttpGet("customer/{userId}")]
    public async Task<IActionResult> GetByCustomerId(int userId)
    {
        var orders = await _orderItemsRepository.GetOrdersByCustomerId(userId);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderItemsRepository.GetOrderById(id);
        if (order is null) return NotFound();
        return Ok(order);
    }
}


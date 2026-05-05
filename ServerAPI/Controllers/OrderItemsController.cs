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


    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomerId(int customerId)
    {
        var orders = await _orderItemsRepository.GetOrdersByCustomerId(customerId);
        return Ok(orders);
    }

}


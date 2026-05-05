using Core;
using Interface;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers;


[ApiController]
[Route("order_items")]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemsRepository _orderItemsRepository;

    public OrderItemsController(IOrderItemsRepository orderItemsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
    }


    [HttpGet()]
}


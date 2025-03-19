using BookTaxi.Common2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookTaxi.Api.Controllers;

[Route("api/drivers/driver_id/[controller]")]
[ApiController]
public class UserCarOrderController : ControllerBase
{
    private readonly UserCarOrderService _orderService;
    public UserCarOrderController(UserCarOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("add-order")]
    public async Task<IActionResult> AddUserCarOrder(UserCarsOrdersDto dtos)
    {
        try
        {
            var order = await _orderService.AddOrder(dtos);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-all-orders")]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _orderService.GetAllOrdes();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
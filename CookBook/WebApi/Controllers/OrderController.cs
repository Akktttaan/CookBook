using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("order")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost("add-order")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> AddOrder([FromBody] OrderDetailData[] orderDetails)
    {
        await _orderService.AddOrder(orderDetails);

        return NoContent();
    }
    
    
    [HttpGet("recipes-for-order")]
    [ProducesResponseType(typeof(OrderDetailData[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetDishRecipesForOrder()
    {
        var query = await _orderService.GetDishRecipesForOrder();

        return Ok(query);
    }
}
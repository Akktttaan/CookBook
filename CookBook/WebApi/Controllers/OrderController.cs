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
    [ProducesResponseType(typeof(OrderData),200)]
    [AllowAnonymous]
    public async Task<IActionResult> AddOrder([FromBody] OrderDetailData[] orderDetails)
    {
        var report = await _orderService.AddOrder(orderDetails);

        return Ok(report);
    }

    [HttpPost("all-order")]
    [ProducesResponseType(typeof(ReportData), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllOrder([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
    {
        var allOrder = await _orderService.GetAllOrder(dateFrom, dateTo);

        return Ok(allOrder);
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
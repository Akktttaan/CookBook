using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("grocery")]
public class GroceryController : Controller
{
    private readonly IGroceryService _groceryService;

    /// <summary>
    /// Контроллер
    /// </summary>
    public GroceryController(IGroceryService groceryService)
    {
        _groceryService = groceryService;
    }

    [HttpGet("item")]
    [ProducesResponseType(typeof(GroceryItemData[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetGroceryItems()
    {
        var query = await _groceryService.GetGroceryItems();

        return Ok(query);
    }

    [HttpDelete("item")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteGroceryItem([FromBody] int id)
    {
        await _groceryService.DeleteGroceryItem(id);

        return NoContent();
    }

    [HttpPost("item")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> AddGroceryItem([FromBody] GroceryItem groceryItem)
    {
        await _groceryService.AddGroceryItem(groceryItem);

        return NoContent();
    }

    [HttpPut("item")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateGroceryItem([FromBody] GroceryItem groceryItem)
    {
        await _groceryService.UpdateGroceryItem(groceryItem);

        return NoContent();
    }

    [HttpGet("unit-of-measure")]
    [ProducesResponseType(typeof(UnitOfMeasure[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetUnitOfMeasures()
    {
        var query = await _groceryService.GetUnitOfMeasures();

        return Ok(query);
    }

    [HttpDelete("unit-of-measure")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteUnitOfMeasure([FromBody] int id)
    {
        await _groceryService.DeleteUnitOfMeasure(id);

        return NoContent();
    }

    [HttpPost("unit-of-measure")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> AddUnitOfMeasure([FromBody] UnitOfMeasure groceryItem)
    {
        await _groceryService.AddUnitOfMeasure(groceryItem);

        return NoContent();
    }

    [HttpPut("unit-of-measure")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateUnitOfMeasure([FromBody] UnitOfMeasure groceryItem)
    {
        await _groceryService.UpdateUnitOfMeasure(groceryItem);

        return NoContent();
    }

    [HttpGet("item-desc")]
    [ProducesResponseType(typeof(GroceryItemData), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetGroceryItemDesc([FromQuery] int id)
    {
        var query = await _groceryService.GetGroceryItem(id);

        return Ok(query);
    }
}
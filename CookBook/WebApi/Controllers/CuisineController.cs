using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("cuisine")]
public class CuisineController : Controller
{
    private readonly ICuisineCategoryService _cuisineCategoryService;

    /// <summary>
    /// Контроллер
    /// </summary>
    public CuisineController(ICuisineCategoryService cuisineCategoryService)
    {
        _cuisineCategoryService = cuisineCategoryService;
    }

    [HttpGet("category")]
    [ProducesResponseType(typeof(CuisineCategory[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetCuisineCategories()
    {
        var query = await _cuisineCategoryService.GetCuisineCategories();

        return Ok(query);
    }

    [HttpDelete("category")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteCuisineCategory([FromBody] int id)
    {
        await _cuisineCategoryService.DeleteCuisineCategory(id);

        return NoContent();
    }

    [HttpPost("category")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> AddCuisineCategory([FromBody] CuisineCategory cuisineCategory)
    {
        await _cuisineCategoryService.AddCuisineCategory(cuisineCategory);

        return NoContent();
    }

    [HttpPut("category")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateCuisineCategory([FromBody] CuisineCategory cuisineCategory)
    {
        await _cuisineCategoryService.UpdateCuisineCategory(cuisineCategory);

        return NoContent();
    }

    [HttpPost("dish-type")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> AddDishType([FromBody] DishType dishType)
    {
        await _cuisineCategoryService.AddDishType(dishType);

        return NoContent();
    }

    [HttpDelete("dish-type")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteDishType([FromBody] int id)
    {
        await _cuisineCategoryService.DeleteDishType(id);

        return NoContent();
    }

    [HttpPut("dish-type")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateDishType([FromBody] DishType dishType)
    {
        await _cuisineCategoryService.UpdateDishType(dishType);

        return NoContent();
    }

    [HttpGet("dish-type")]
    [ProducesResponseType(typeof(DishType[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetDishTypes()
    {
        var query = await _cuisineCategoryService.GetDishTypes();

        return Ok(query);
    }
}
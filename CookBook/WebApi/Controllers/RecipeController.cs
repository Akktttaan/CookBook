using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("recipe")]
public class RecipeController : Controller
{
    private readonly IRecipeService _recipeService;

    /// <summary>
    /// Контроллер
    /// </summary>
    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet("recipe")]
    [ProducesResponseType(typeof(DishRecipeData[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetDishRecipes([FromQuery] int dishTypeId, [FromQuery] int cuisineCategoryId)
    {
        var query = await _recipeService.GetDishRecipes(dishTypeId, cuisineCategoryId);

        return Ok(query);
    }

    [HttpDelete("recipe")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteDishRecipe([FromBody] int id)
    {
        await _recipeService.DeleteDishRecipe(id);

        return NoContent();
    }

    [HttpPost("recipe")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> AddDishRecipe([FromBody] DishRecipeData dishRecipe)
    {
        await _recipeService.AddDishRecipe(dishRecipe);

        return NoContent();
    }

    [HttpPut("recipe")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateDishRecipe([FromBody] DishRecipeData dishRecipe)
    {
        await _recipeService.UpdateDishRecipe(dishRecipe);

        return NoContent();
    }

    [HttpGet("ingredient")]
    [ProducesResponseType(typeof(RecipeIngredientData[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetRecipeIngredients([FromQuery] int dishRecipeId)
    {
        var query = await _recipeService.GetRecipeIngredients(dishRecipeId);

        return Ok(query);
    }

    [HttpDelete("ingredient")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteRecipeIngredient([FromBody] int id)
    {
        await _recipeService.DeleteRecipeIngredient(id);

        return NoContent();
    }

    [HttpPost("ingredient")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> AddRecipeIngredient([FromBody] RecipeIngredient dishRecipe)
    {
        await _recipeService.AddRecipeIngredient(dishRecipe);

        return NoContent();
    }

    [HttpPut("ingredient")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateRecipeIngredient([FromBody] RecipeIngredient dishRecipe)
    {
        await _recipeService.UpdateRecipeIngredient(dishRecipe);

        return NoContent();
    }

    [HttpGet("get-recipe")]
    [ProducesResponseType(typeof(DishRecipe), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetDishRecipe([FromQuery] int id)
    {
        var query = await _recipeService.GetDishRecipe(id);

        return Ok(query);
    }
}
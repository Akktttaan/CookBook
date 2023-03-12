using Domain.Dto;
using Domain.Models;

namespace Services.Interfaces;

public interface IRecipeService
{
    public Task<DishRecipe> GetDishRecipe(int id);

    public Task DeleteRecipeIngredient(int id);

    public Task UpdateRecipeIngredient(RecipeIngredient dishType);

    public Task AddRecipeIngredient(RecipeIngredient dishType);

    public Task<RecipeIngredientData[]> GetRecipeIngredients(int dishRecipeId);

    public Task DeleteDishRecipe(int id);

    public Task UpdateDishRecipe(DishRecipeData dishRecipe);

    public Task AddDishRecipe(DishRecipeData dishRecipe);

    public Task<DishRecipeData[]> GetDishRecipes(int dishTypeId, int cuisineCategoryId);

}
using Dal.Interfaces;
using Domain.Dto;
using Domain.Models;
using Services.Interfaces;

namespace Services.Services;

public class RecipeService : IRecipeService
{
    private readonly IUnitOfWork _unitOfWork;

    public RecipeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DishRecipeData[]> GetDishRecipes(int dishTypeId, int cuisineCategoryId)
    {
        var recipeRepository = _unitOfWork.GetRepository<DishRecipe>();
        var query = await recipeRepository.GetAll();

        return query
            .Where(x => x.DishTypeId == dishTypeId &&
                        x.CuisineCategoryId == cuisineCategoryId)
            .Select(recipe => new DishRecipeData()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Price = recipe.Price
            })
            .ToArray();
    }

    public async Task AddDishRecipe(DishRecipeData dishRecipe)
    {
        var dishRecipeRep = _unitOfWork.GetRepository<DishRecipe>();
        var recipeIngredientRep = _unitOfWork.GetRepository<RecipeIngredient>();
        var model = new DishRecipe()
        {
            CuisineCategoryId = dishRecipe.CuisineCategoryId,
            Name = dishRecipe.Name,
            Price = dishRecipe.Price,
            DishTypeId = dishRecipe.DishTypeId
        };

        await dishRecipeRep.Add(model);
        var dishRecipes = await dishRecipeRep.GetAll();
        var dishRecipeId = dishRecipes
            .FirstOrDefault(x => x.Name == dishRecipe.Name)!
            .Id;
        foreach (var ingredientModel in dishRecipe.RecipeIngredients.Select(ingredient => new RecipeIngredient()
                 {
                     DishRecipeId = dishRecipeId.Value,
                     GroceryItemId = ingredient.GroceryItemId,
                     Quantity = ingredient.Quantity
                 }))
        {
            await recipeIngredientRep.Add(ingredientModel);
        }
    }

    public async Task UpdateDishRecipe(DishRecipeData dishRecipe)
    {
        var dishRecipeRep = _unitOfWork.GetRepository<DishRecipe>();
        var recipeIngredientRep = _unitOfWork.GetRepository<RecipeIngredient>();
        var model = new DishRecipe()
        {
            Id = dishRecipe.Id,
            CuisineCategoryId = dishRecipe.CuisineCategoryId,
            Name = dishRecipe.Name,
            Price = dishRecipe.Price,
            DishTypeId = dishRecipe.DishTypeId
        };

        await dishRecipeRep.Update(model);
        var currentIngredients = await recipeIngredientRep.GetAll();
            
        foreach (var deleteId in currentIngredients
                     .Where(x => x.DishRecipeId == dishRecipe.Id)
                     .Select(x => x.Id)
                     .Except(dishRecipe.RecipeIngredients.Select(x => x.Id)))
        {
            await recipeIngredientRep.Delete(deleteId.Value);
        }
        
        foreach (var ingredientModel in dishRecipe.RecipeIngredients.Select(ingredient => new RecipeIngredient()
                 {
                     Id = ingredient.Id,
                     DishRecipeId = ingredient.DishRecipeId,
                     GroceryItemId = ingredient.GroceryItemId,
                     Quantity = ingredient.Quantity
                 }))
        {
            if (ingredientModel.Id is not null)
            {
                await recipeIngredientRep.Update(ingredientModel);
            }
            else
            {
                await recipeIngredientRep.Add(ingredientModel);
            }
        }
    }

    public async Task DeleteDishRecipe(int id)
    {
        await _unitOfWork.GetRepository<DishRecipe>().Delete(id);
    }

    public async Task<RecipeIngredientData[]> GetRecipeIngredients(int dishRecipeId)
    {
        var ingredientRepository = _unitOfWork.GetRepository<RecipeIngredient>();
        var groceryRepository = _unitOfWork.GetRepository<GroceryItem>();
        var unitOfMeasureRepository = _unitOfWork.GetRepository<UnitOfMeasure>();
        var query = await ingredientRepository.GetAll();

        return query
            .Where(x => x.DishRecipeId == dishRecipeId)
            .Select(x => new RecipeIngredientData()
            {
                Id = x.Id,
                DishRecipeId = x.DishRecipeId,
                GroceryItemId = x.GroceryItemId,
                Quantity = x.Quantity,
                GroceryItemDescription = groceryRepository.GetById(x.GroceryItemId).Result.Name,
                GroceryItemUnitOfMeasure = unitOfMeasureRepository
                    .GetById(groceryRepository
                        .GetById(x.GroceryItemId).Result.UnitOfMeasureId.Value).Result.Name
            }).ToArray();
    }

    public async Task AddRecipeIngredient(RecipeIngredient dishType)
    {
        await _unitOfWork.GetRepository<RecipeIngredient>().Add(dishType);
    }

    public async Task UpdateRecipeIngredient(RecipeIngredient dishType)
    {
        await _unitOfWork.GetRepository<RecipeIngredient>().Update(dishType);
    }

    public async Task DeleteRecipeIngredient(int id)
    {
        await _unitOfWork.GetRepository<RecipeIngredient>().Delete(id);
    }

    public async Task<DishRecipe> GetDishRecipe(int id)
    {
        var query = await _unitOfWork.GetRepository<DishRecipe>().GetById(id);

        return query;
    }
}
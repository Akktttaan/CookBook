namespace Domain.Dto;

public class RecipeIngredientData
{
    /// <summary>
    /// Идентификатор ингредиента рецепта.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Идентификатор рецепта блюда, к которому относится ингредиент.
    /// </summary>
    public int DishRecipeId { get; set; }

    /// <summary>
    /// Идентификатор продукта, который является ингредиентом.
    /// </summary>
    public int GroceryItemId { get; set; }

    public GroceryItemData GroceryItem { get; set; }

    /// <summary>
    /// Количество ингредиента.
    /// </summary>
    public int Quantity { get; set; }
}
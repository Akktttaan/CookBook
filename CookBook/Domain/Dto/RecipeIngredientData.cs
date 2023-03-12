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

    /// <summary>
    /// Описание продукта
    /// </summary>
    public string GroceryItemDescription { get; set; }

    /// <summary>
    /// Количество ингредиента.
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Единица измерения продукта
    /// </summary>
    public string GroceryItemUnitOfMeasure { get; set; }
}
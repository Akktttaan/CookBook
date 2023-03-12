namespace Domain.Models;

/// <summary>
/// Рецепт блюда
/// </summary>
public class DishRecipe : IBaseEntity
{
    /// <summary>
    /// Идентификатор рецепта блюда.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Название блюда.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Идентификатор категории кухни, к которой относится блюдо.
    /// </summary>
    public int CuisineCategoryId { get; set; }
    
    /// <summary>
    /// Тип блюда
    /// </summary>
    public int DishTypeId { get; set; }

    /// <summary>
    /// Цена блюда.
    /// </summary>
    public decimal Price { get; set; }
}
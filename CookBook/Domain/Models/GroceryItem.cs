namespace Domain.Models;

/// <summary>
/// Продукт
/// </summary>
public class GroceryItem : IBaseEntity
{
    /// <summary>
    /// Идентификатор продукта.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Название продукта.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Идентификатор единицы измерения продукта.
    /// </summary>
    public int? UnitOfMeasureId { get; set; }

    /// <summary>
    /// Цена продукта.
    /// </summary>
    public decimal Price { get; set; }
}
namespace Domain.Dto;

public class GroceryItemData
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
    /// Описание единицы измерения продукта.
    /// </summary>
    public string UnitOfMeasureDescription { get; set; }

    /// <summary>
    /// Цена продукта.
    /// </summary>
    public decimal Price { get; set; }
}
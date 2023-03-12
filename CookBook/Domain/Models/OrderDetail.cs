namespace Domain.Models;

/// <summary>
/// Детали заказа
/// </summary>
public class OrderDetail : IBaseEntity
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Идентификатор блюда
    /// </summary>
    public int DishRecipeId { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; set; }
}
namespace Domain.Dto;

public class OrderDetailData
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Блюдо
    /// </summary>
    public DishRecipeData DishRecipe { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; set; }
}
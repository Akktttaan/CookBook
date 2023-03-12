namespace Domain.Models;

/// <summary>
/// Заказ
/// </summary>
public class Order : IBaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата заказа
    /// </summary>
    public DateTime OrderDate { get; set; }
    
    /// <summary>
    /// Ключ заказа
    /// </summary>
    public Guid OrderKey { get; set; }
}
namespace Domain.Models;

/// <summary>
/// Тип блюда
/// </summary>
public class DishType : IBaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }
}
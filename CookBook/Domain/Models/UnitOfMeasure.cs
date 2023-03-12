namespace Domain.Models;

/// <summary>
/// Единица измерения
/// </summary>
public class UnitOfMeasure : IBaseEntity
{
    /// <summary>
    /// Идентификатор единицы измерения.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Название единицы измерения.
    /// </summary>
    public string Name { get; set; }
}
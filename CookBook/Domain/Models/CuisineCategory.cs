namespace Domain.Models;

/// <summary>
/// Тип блюда
/// </summary>
public class CuisineCategory : IBaseEntity
{
    /// <summary>
    /// Идентификатор категории кухни.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Название категории кухни.
    /// </summary>
    public string Name { get; set; }
}
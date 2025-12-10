namespace Template.Application.Models.Common;

/// <summary>
/// Результат с пагинацией
/// </summary>
public sealed class PagedResult<T>
{
    /// <summary>
    /// Элементы текущей страницы
    /// </summary>
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();

    /// <summary>
    /// Текущая страница
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Общее количество элементов
    /// </summary>
    public int TotalCount { get; init; }
}

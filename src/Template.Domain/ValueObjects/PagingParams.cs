namespace Template.Domain.ValueObjects;

/// <summary>
/// Value‑object, содержащий параметры пагинации для запросов к репозиториям.
/// </summary>
public sealed record PagingParams
{
    /// <summary>
    /// Номер текущей страницы (начинается с 1).
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Размер страницы (количество элементов).
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Вычисляемое количество элементов, которое следует пропустить
    /// (используется в LINQ‑операциях <c>Skip</c>).
    /// </summary>
    public int Skip => (PageNumber - 1) * PageSize;
}

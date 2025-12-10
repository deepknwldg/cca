namespace Template.Api.Models.Common;

/// <summary>
/// HTTP запрос с параметрами пагинации
/// </summary>
public sealed class PagedRequest
{
    /// <summary>
    /// Номер страницы
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Количество элементов на странице
    /// </summary>
    public int PageSize { get; set; } = 10;
}

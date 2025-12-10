namespace Template.Domain.ValueObjects;

/// <summary>
/// Value object для параметров пагинации
/// </summary>
public sealed record PagingParams
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Skip => (PageNumber - 1) * PageSize;
}

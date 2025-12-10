namespace Template.Api.Models.Course;

/// <summary>
/// Модель запроса для обновления существующего курса.
/// </summary>
public class UpdateCourseRequest
{
    /// <summary>
    /// Новое название курса. Обязательно.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Необязательное новое описание курса.
    /// </summary>
    public string? Description { get; set; }
}

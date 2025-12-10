
namespace Template.Api.Models.Course;

/// <summary>
/// Модель запроса для создания нового курса.
/// </summary>
public class CreateCourseRequest
{
    /// <summary>
    /// Название курса. Обязательно.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Необязательное описание курса.
    /// </summary>
    public string? Description { get; set; }
}

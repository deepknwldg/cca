namespace Template.Api.Models.Lesson;

/// <summary>
/// Модель запроса для создания нового урока.
/// </summary>
public class CreateLessonRequest
{
    /// <summary>
    /// Идентификатор курса, к которому относится урок.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    /// Заголовок урока.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Содержимое урока.
    /// </summary>
    public string Content { get; set; } = default!;
}

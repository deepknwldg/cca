namespace Template.Api.Models.Lesson;

/// <summary>
/// Модель запроса для обновления существующего урока.
/// </summary>
public class UpdateLessonRequest
{
    /// <summary>
    /// Новый заголовок урока.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Новое содержимое урока.
    /// </summary>
    public string Content { get; set; } = default!;
}

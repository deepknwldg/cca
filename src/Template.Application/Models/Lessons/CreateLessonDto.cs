namespace Template.Application.Models.Lessons;

/// <summary>
/// DTO, получаемый от клиента при создании нового урока.
/// </summary>
public class CreateLessonDto
{
    /// <summary>
    /// Идентификатор курса, к которому относится урок.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    /// Заголовок урока. Обязательно.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Содержимое урока. Обязательно.
    /// </summary>
    public string Content { get; set; } = default!;
}

namespace Template.Application.Models.Lessons;

/// <summary>
/// DTO, получаемый от клиента для обновления существующего урока.
/// </summary>
public class UpdateLessonDto
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

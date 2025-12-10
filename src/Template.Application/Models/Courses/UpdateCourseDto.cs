namespace Template.Application.Models.Courses;

/// <summary>
/// DTO, получаемый от клиента при обновлении курса.
/// </summary>
public class UpdateCourseDto
{
    /// <summary>
    /// Новое название курса. Обязательно.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Новое (или прежнее) описание курса.
    /// </summary>
    public string? Description { get; set; }
}

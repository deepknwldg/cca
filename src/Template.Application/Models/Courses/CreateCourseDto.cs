namespace Template.Application.Models.Courses;

/// <summary>
/// DTO, получаемый от клиента при создании курса.
/// </summary>
public class CreateCourseDto
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

namespace Template.Application.Models.Enrollments;

/// <summary>
/// DTO, получаемый от клиента для записи пользователя на курс.
/// </summary>
public class EnrollUserDto
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор курса.
    /// </summary>
    public Guid CourseId { get; set; }
}

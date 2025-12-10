namespace Template.Api.Models.Enrollments;

/// <summary>
/// Модель запроса для записи пользователя на курс.
/// </summary>
public class EnrollUserRequest
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

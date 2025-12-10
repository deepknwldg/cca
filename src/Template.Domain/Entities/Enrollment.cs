namespace Template.Domain.Entities;

/// <summary>
/// Сущность, связывающая пользователя (<see cref="User"/>) и курс (<see cref="Course"/>).
/// Описывает факт записи пользователя на курс и дату записи.
/// </summary>
public class Enrollment
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Навигационное свойство к пользователю.
    /// </summary>
    public User User { get; set; } = default!;

    /// <summary>
    /// Идентификатор курса.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    /// Навигационное свойство к курсу.
    /// </summary>
    public Course Course { get; set; } = default!;

    /// <summary>
    /// Дата и время записи пользователя на курс (UTC).
    /// </summary>
    public DateTime EnrolledAt { get; set; }
}

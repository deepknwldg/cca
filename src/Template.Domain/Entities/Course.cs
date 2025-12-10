namespace Template.Domain.Entities;

/// <summary>
/// Сущность, представляющая учебный курс. Содержит набор уроков
/// (<see cref="Lesson"/>) и записи о пользователях, записанных на курс
/// (<see cref="Enrollment"/>).
/// </summary>
public class Course
{
    /// <summary>
    /// Идентификатор курса.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название курса.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Подробное описание курса.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Коллекция уроков, принадлежащих данному курсу (отношение 1 : N).
    /// </summary>
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    /// <summary>
    /// Коллекция записей о пользователях, записанных на курс (отношение M : N).
    /// </summary>
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

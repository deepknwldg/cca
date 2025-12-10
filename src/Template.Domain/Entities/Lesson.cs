namespace Template.Domain.Entities;

/// <summary>
/// Сущность, представляющая отдельный учебный материал (урок) внутри курса.
/// </summary>
public class Lesson
{
    /// <summary>
    /// Идентификатор урока.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Заголовок урока.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Содержимое урока (текст, markdown и т.п.).
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Идентификатор курса, к которому относится урок.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    /// Навигационное свойство к родительскому курсу.
    /// </summary>
    public Course Course { get; set; } = default!;
}

namespace Template.Domain.Entities;

/// <summary>
/// Сущность, представляющая пользователя системы.
/// Содержит данные аутентификации, профиль и записи о записанных курсах.
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Электронная почта (уникальное имя входа).
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Хеш пароля (не хранится в открытом виде).
    /// </summary>
    public string PasswordHash { get; set; } = default!;

    /// <summary>
    /// Профиль пользователя (1 : 1 связь).
    /// </summary>
    public UserProfile Profile { get; set; } = default!;

    /// <summary>
    /// Коллекция записей о курсах, на которые пользователь записан (отношение M : N).
    /// </summary>
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

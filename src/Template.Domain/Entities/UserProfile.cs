namespace Template.Domain.Entities;

/// <summary>
/// Профиль пользователя, содержащий персональные данные. Существует в 1 : 1 связи с <see cref="User"/>.
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Идентификатор профиля (совпадает с Id пользователя).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Идентификатор пользователя, к которому привязан профиль.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Навигационное свойство обратно к пользователю.
    /// </summary>
    public User User { get; set; } = default!;
}

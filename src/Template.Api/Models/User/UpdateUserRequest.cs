namespace Template.Api.Models.User;

/// <summary>
/// Модель запроса для частичного/полного обновления данных пользователя.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Новая электронная почта пользователя.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Новый пароль пользователя. Может быть <c>null</c>, если пароль не меняется.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Новое имя пользователя.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Новая фамилия пользователя.
    /// </summary>
    public string LastName { get; set; } = default!;
}

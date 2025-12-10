namespace Template.Api.Models.User;

/// <summary>
/// Модель запроса для создания нового пользователя.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Пароль пользователя в открытом виде (будет захеширован).
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string LastName { get; set; } = default!;
}

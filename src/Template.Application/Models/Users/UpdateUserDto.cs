namespace Template.Application.Models.Users;

/// <summary>
/// DTO, получаемый от клиента при обновлении данных пользователя.
/// </summary>
public class UpdateUserDto
{
    /// <summary>
    /// Новая электронная почта.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Новый захешированный пароль. Может быть <c>null</c>, если пароль не меняется.
    /// </summary>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Новое имя пользователя.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Новая фамилия пользователя.
    /// </summary>
    public string LastName { get; set; } = default!;
}

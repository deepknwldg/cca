namespace Template.Application.Models.Users;

/// <summary>
/// DTO, получаемый от клиента при регистрации нового пользователя.
/// </summary>
public class CreateUserDto
{
    /// <summary>
    /// Электронная почта пользователя (обязательно).
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Пароль в открытом виде (будет захеширован).
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Имя пользователя (обязательно).
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Фамилия пользователя (обязательно).
    /// </summary>
    public string LastName { get; set; } = default!;
}

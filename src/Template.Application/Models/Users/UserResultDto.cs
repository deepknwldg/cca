namespace Template.Application.Models.Users;

/// <summary>
/// DTO, возвращаемый клиенту после создания/запроса пользователя.
/// </summary>
public class UserResultDto
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Имя.</summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; } = default!;
}

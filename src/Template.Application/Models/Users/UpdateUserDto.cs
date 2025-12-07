namespace Template.Application.Models.Users;

public class UpdateUserDto
{
    public string Email { get; set; } = default!;
    public string? PasswordHash { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}

namespace Template.Application.Models.Users;

public record UserResultDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName
);

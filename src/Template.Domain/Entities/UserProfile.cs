namespace Template.Domain.Entities;

public class UserProfile
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    // 1:1 back reference
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}

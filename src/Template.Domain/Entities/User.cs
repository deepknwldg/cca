namespace Template.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    // 1:1
    public UserProfile Profile { get; set; } = default!;

    // M:N
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

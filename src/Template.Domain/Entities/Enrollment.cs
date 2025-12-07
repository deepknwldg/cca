namespace Template.Domain.Entities;

public class Enrollment
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;

    public DateTime EnrolledAt { get; set; }
}

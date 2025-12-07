namespace Template.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    // 1:N
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    // M:N
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

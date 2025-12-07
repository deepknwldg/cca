namespace Template.Domain.Entities;

public class Lesson
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;
}

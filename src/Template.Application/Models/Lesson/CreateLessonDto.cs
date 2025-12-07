namespace Template.Application.Models.Lesson;

public class CreateLessonDto
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}

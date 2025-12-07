namespace Template.Api.Models.Lesson;

public class CreateLessonRequest
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}

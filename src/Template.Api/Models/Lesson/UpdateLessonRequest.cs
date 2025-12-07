namespace Template.Api.Models.Lesson;

public class UpdateLessonRequest
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}

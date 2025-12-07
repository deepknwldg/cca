namespace Template.Api.Models.Course;

public class UpdateCourseRequest
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}

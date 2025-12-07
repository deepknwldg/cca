
namespace Template.Api.Models.Course;

public class CreateCourseRequest
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}

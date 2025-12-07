namespace Template.Application.Models.Courses;

public class UpdateCourseDto
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}

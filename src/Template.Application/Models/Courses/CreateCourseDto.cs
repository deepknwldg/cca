namespace Template.Application.Models.Courses;

public class CreateCourseDto
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}

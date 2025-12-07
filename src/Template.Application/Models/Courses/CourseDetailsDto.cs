namespace Template.Application.Models.Courses;

public record CourseDetailsDto(
    Guid Id,
    string Title,
    string? Description,
    IReadOnlyList<string> Lessons
);

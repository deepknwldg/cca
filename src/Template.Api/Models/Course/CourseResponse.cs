namespace Template.Api.Models.Course;

public record CourseResponse(
    Guid Id,
    string Title,
    string? Description
);

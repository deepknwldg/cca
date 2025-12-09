namespace Template.Application.Models.Lessons;

public record LessonResultDto(
    Guid Id,
    Guid CourseId,
    string Title,
    string Content
);

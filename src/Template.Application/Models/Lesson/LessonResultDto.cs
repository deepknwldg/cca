namespace Template.Application.Models.Lesson;

public record LessonResultDto(
    Guid Id,
    Guid CourseId,
    string Title,
    string Content
);

namespace Template.Application.Models.Lessons;

/// <summary>
/// DTO, возвращаемый клиенту при запросе данных об уроке.
/// </summary>
/// <param name="Id">Идентификатор урока.</param>
/// <param name="CourseId">Идентификатор курса, к которому относится урок.</param>
/// <param name="Title">Заголовок урока.</param>
/// <param name="Content">Содержимое урока.</param>
public record LessonResultDto(
    Guid Id,
    Guid CourseId,
    string Title,
    string Content
);

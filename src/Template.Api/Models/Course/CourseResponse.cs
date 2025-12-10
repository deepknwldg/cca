namespace Template.Api.Models.Course;

/// <summary>
/// DTO, возвращаемый клиенту при запросе информации о курсе.
/// </summary>
/// <param name="Id">Уникальный идентификатор курса.</param>
/// <param name="Title">Название курса.</param>
/// <param name="Description">Необязательное описание курса.</param>
public record CourseResponse(
    Guid Id,
    string Title,
    string? Description
);

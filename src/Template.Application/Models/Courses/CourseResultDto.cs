namespace Template.Application.Models.Courses;

/// <summary>
/// DTO, возвращаемый клиенту при запросе списка или отдельного курса.
/// </summary>
/// <param name="Id">Идентификатор курса.</param>
/// <param name="Title">Название курса.</param>
/// <param name="Description">Необязательное описание курса.</param>
public record CourseResultDto(
    Guid Id,
    string Title,
    string? Description
);

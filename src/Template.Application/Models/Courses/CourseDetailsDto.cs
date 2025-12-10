namespace Template.Application.Models.Courses;

/// <summary>
/// DTO, представляющий детализированную информацию о курсе,
/// включая список названий уроков.
/// </summary>
/// <param name="Id">Идентификатор курса.</param>
/// <param name="Title">Название курса.</param>
/// <param name="Description">Необязательное описание курса.</param>
/// <param name="Lessons">Коллекция названий уроков, принадлежащих курсу.</param>
public record CourseDetailsDto(
    Guid Id,
    string Title,
    string? Description,
    IReadOnlyList<string> Lessons
);

using Template.Application.Models.Lessons;

namespace Template.Application.Abstractions.Services;

/// <summary>
/// Сервис, инкапсулирующий бизнес‑логику работы с уроками.
/// </summary>
public interface ILessonService
{
    /// <summary>
    /// Создать новый урок.
    /// </summary>
    /// <param name="dto">Данные урока.</param>
    /// <returns>Созданный урок.</returns>
    Task<LessonResultDto> CreateAsync(CreateLessonDto dto);

    /// <summary>
    /// Удалить урок.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns><c>true</c>, если удалено; иначе <c>false</c>.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Получить все уроки, принадлежащие конкретному курсу.
    /// </summary>
    /// <param name="courseId">Идентификатор курса.</param>
    /// <returns>Список уроков.</returns>
    Task<IReadOnlyList<LessonResultDto>> GetByCourseAsync(Guid courseId);

    /// <summary>
    /// Получить урок по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns>Урок или <c>null</c>, если не найден.</returns>
    Task<LessonResultDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Обновить данные урока.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <param name="dto">Новые данные.</param>
    /// <returns><c>true</c>, если обновление прошло успешно; иначе <c>false</c>.</returns>
    Task<bool> UpdateAsync(Guid id, UpdateLessonDto dto);
}

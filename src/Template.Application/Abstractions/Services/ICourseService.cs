using Template.Application.Models.Courses;

namespace Template.Application.Abstractions.Services;

/// <summary>
/// Сервисный слой, предоставляющий бизнес‑логику для работы с курсами.
/// </summary>
public interface ICourseService
{
    /// <summary>
    /// Создаёт новый курс.
    /// </summary>
    /// <param name="dto">Данные для создания курса.</param>
    /// <returns>Созданный курс.</returns>
    Task<CourseResultDto> CreateAsync(CreateCourseDto dto);

    /// <summary>
    /// Получает курс по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор курса.</param>
    /// <returns>Курс, если найден; иначе <c>null</c>.</returns>
    Task<CourseResultDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Возвращает список всех курсов.
    /// </summary>
    Task<IReadOnlyList<CourseResultDto>> GetAllAsync();

    /// <summary>
    /// Обновляет курс.
    /// </summary>
    /// <param name="id">Идентификатор курса, который необходимо обновить.</param>
    /// <param name="dto">Новые данные курса.</param>
    /// <returns>True — если курс найден и обновлён; иначе false.</returns>
    Task<bool> UpdateAsync(Guid id, UpdateCourseDto dto);

    /// <summary>
    /// Удаляет курс.
    /// </summary>
    /// <param name="id">Идентификатор курса для удаления.</param>
    /// <returns>True — если курс найден и удалён; иначе false.</returns>
    Task<bool> DeleteAsync(Guid id);
}

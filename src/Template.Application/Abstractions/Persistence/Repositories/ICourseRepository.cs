using Template.Domain.Entities;

namespace Template.Application.Abstractions.Persistence.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="Course"/>.
/// Наследует базовый <see cref="IRepository{Course}"/> и добавляет специфичные запросы.
/// </summary>
public interface ICourseRepository : IRepository<Course>
{
    /// <summary>
    /// Получить курс вместе с его уроками.
    /// </summary>
    /// <param name="id">Идентификатор курса.</param>
    /// <returns>
    /// Курс с заполненным навигационным свойством <c>Lessons</c>,
    /// либо <c>null</c>, если курс не найден.
    /// </returns>
    Task<Course?> GetWithLessonsAsync(Guid id);
}

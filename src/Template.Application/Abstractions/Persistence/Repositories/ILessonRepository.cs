using Template.Domain.Entities;

namespace Template.Application.Abstractions.Persistence.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="Lesson"/>.
/// </summary>
public interface ILessonRepository : IRepository<Lesson>
{
    /// <summary>
    /// Получить все уроки, принадлежащие указанному курсу.
    /// </summary>
    /// <param name="courseId">Идентификатор курса.</param>
    /// <returns>Коллекция уроков.</returns>
    Task<IReadOnlyList<Lesson>> GetByCourseIdAsync(Guid courseId);
}

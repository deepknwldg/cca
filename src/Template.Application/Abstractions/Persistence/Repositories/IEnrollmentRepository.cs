using Template.Domain.Entities;

namespace Template.Application.Abstractions.Persistence.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="Enrollment"/> (запись пользователя на курс).
/// </summary>
public interface IEnrollmentRepository
{
    /// <summary>
    /// Добавить новую запись о записи пользователя на курс.
    /// </summary>
    /// <param name="enrollment">Объект <see cref="Enrollment"/>.</param>
    Task AddAsync(Enrollment enrollment);

    /// <summary>
    /// Получить запись о записи пользователя на курс.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="courseId">Идентификатор курса.</param>
    /// <returns>
    /// Найденный <see cref="Enrollment"/> либо <c>null</c>, если такой записи нет.
    /// </returns>
    Task<Enrollment?> GetAsync(Guid userId, Guid courseId);

    /// <summary>
    /// Удалить запись о записи пользователя на курс.
    /// </summary>
    /// <param name="enrollment">Объект, который нужно удалить.</param>
    void Remove(Enrollment enrollment);
}

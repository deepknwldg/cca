using Template.Application.Models.Enrollments;

namespace Template.Application.Abstractions.Services;

/// <summary>
/// Сервис бизнес‑логики, отвечающий за запись и удаление записей
/// о том, какие пользователи записаны на какие курсы.
/// </summary>
public interface IEnrollmentService
{
    /// <summary>
    /// Записать пользователя на курс.
    /// </summary>
    /// <param name="dto">Данные для записи.</param>
    /// <returns>Результат операции (идентификатор записи и статус).</returns>
    Task<EnrollmentResultDto> EnrollAsync(EnrollUserDto dto);

    /// <summary>
    /// Снять пользователя с курса.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="courseId">Идентификатор курса.</param>
    /// <returns><c>true</c>, если запись удалена; иначе <c>false</c>.</returns>
    Task<bool> RemoveAsync(Guid userId, Guid courseId);
}

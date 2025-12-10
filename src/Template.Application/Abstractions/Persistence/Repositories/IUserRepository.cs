using Template.Domain.Entities;

namespace Template.Application.Abstractions.Persistence.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="User"/>.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Получить пользователя вместе с профилем (например, ролями, настройками).
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь с загруженным профилем либо <c>null</c>.</returns>
    Task<User?> GetWithProfileAsync(Guid id, CancellationToken cancellationToken = default);
}

using Template.Application.Models.Common;
using Template.Application.Models.Users;
using Template.Domain.ValueObjects;

namespace Template.Application.Abstractions.Services;

/// <summary>
/// Сервис, предоставляющий операции над пользователями (CRUD, пагинация).
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Создать нового пользователя.
    /// </summary>
    /// <param name="dto">Данные для создания.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданный пользователь.</returns>
    Task<UserResultDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь либо <c>null</c>, если не найден.</returns>
    Task<UserResultDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить всех пользователей постранично.
    /// </summary>
    /// <param name="paging">Параметры пагинации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Постраничный результат.</returns>
    Task<PagedResult<UserResultDto>> GetAllAsync(PagingParams paging, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить данные пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="dto">Новые данные.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><c>true</c>, если пользователь найден и обновлён; иначе <c>false</c>.</returns>
    Task<bool> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns><c>true</c>, если пользователь найден и удалён; иначе <c>false</c>.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

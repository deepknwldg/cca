using Template.Application.Models.Common;
using Template.Domain.ValueObjects;

namespace Template.Application.Abstractions.Persistence.Repositories;

/// <summary>
/// Универсальный репозиторий для работы с любой сущностью домена.
/// </summary>
/// <typeparam name="TEntity">Тип сущности (должен быть классом).</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Получить сущность по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность либо <c>null</c>, если её нет.</returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все сущности из хранилища.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Только для чтения список всех сущностей.</returns>
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить постраничный набор сущностей.
    /// </summary>
    /// <param name="paging">Параметры пагинации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат пагинации.</returns>
    Task<PagedResult<TEntity>> GetPagedAsync(PagingParams paging, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавить новую сущность в хранилище.
    /// </summary>
    /// <param name="entity">Сущность для добавления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить существующую сущность.
    /// </summary>
    /// <param name="entity">Сущность с новыми данными.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Удалить сущность из хранилища.
    /// </summary>
    /// <param name="entity">Сущность, которую нужно удалить.</param>
    void Remove(TEntity entity);
}

namespace Template.Application.Abstractions.Persistence.Repositories;

/// <summary>
/// Интерфейс единицы работы (Unit of Work). Позволяет управлять транзакциями
/// и сохранять изменения в репозиториях как единое целое.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Начать новую транзакцию.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Зафиксировать текущую транзакцию.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Откатить текущую транзакцию.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task RollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Сохранить все изменения в контексте данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Количество изменённых записей.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
